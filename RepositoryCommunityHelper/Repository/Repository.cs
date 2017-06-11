using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.WebService;

namespace RepositoryCommunityHelper.Repository
{
    public class Repository : BaseMagic, IRepository
    {
        private readonly IService _restClient;
        private readonly IMapper _mapper;
        private readonly ConverterJson converter = new ConverterJson();

        private IEnumerable<Player> _players { get; set; }
        private IEnumerable<RequestResource> _requestResources { get; set; }
        private ObservableCollection<RequestResourceDto> _requestResourceDtos;
        private IEnumerable<Faction> _factions { get; set; }
        private ObservableCollection<FactionDto> _factionDtos;
        private ObservableCollection<PlayerDto> _playerDtos;

        //public IEnumerable<RequestResourceDto> RequestResourceDtos { get; set; }

        private Auth auth { get; }

        public string Auth()
        {
            auth.DoAuth();
            string temp = auth.GetMe();
            return temp;
            //return null;
        }

        public void UnAuth()
        {
            auth.UnAuth();
        }

        private TimerCallback timeCB;
        private Timer Timer1;

        private bool _connected = false;

        public bool Connected
        {
            get { return _connected; }
            set
            {
                _connected = value;
                RaisePropertyChanged(nameof(Connected));
            }
        }

        public Repository(IService restClient, IMapper mapper, Auth auth)
        {
            if (restClient == null)
            {
                throw new ArgumentNullException(nameof(restClient));
            }
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
            _mapper = mapper;
            _restClient = restClient;
            _players = new ObservableCollection<Player>();
            _requestResources = new ObservableCollection<RequestResource>();
            _factions = new ObservableCollection<Faction>();
            this.auth = auth;
            //Auth();
            timeCB = new TimerCallback(TimeTick);

            //Timer1 = new Timer(timeCB, null, 5000, 5000);
            RequestResourceDtos = new ObservableCollection<RequestResourceDto>();
            //RequestResourceDtos.CollectionChanged += NotifyCollectionChanged;
            FactionDtos = new ObservableCollection<FactionDto>();
            PlayerDtos = new ObservableCollection<PlayerDto>();

        }

        
        /*public void NotifyCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //This will get called when the collection is changed
            //RaisePropertyChanged("RequestResourceDtos");
        }*/

        public ObservableCollection<RequestResourceDto> RequestResourceDtos //{ get; set; }
        {
            get
            {
                //_requestResourceDtos = GetRequestResourceDtos();
                return _requestResourceDtos;
            }
            set
            {
                _requestResourceDtos = value; 
                RaisePropertyChanged(nameof(RequestResourceDtos));
                //NotifyCollectionChanged(this, null);
            }
        }

        public ObservableCollection<FactionDto> FactionDtos
        {
            get { return _factionDtos; }
            set
            {
                _factionDtos = value;
                RaisePropertyChanged(nameof(FactionDtos));
            }
        }

        public ObservableCollection<PlayerDto> PlayerDtos
        {
            get { return _playerDtos; }
            set
            {
                _playerDtos = value;
                RaisePropertyChanged(nameof(PlayerDtos));
            }
        }

        public PlayerDto FindPlayerDtoById(int id)
        {
            foreach (var playerDto in GetPlayerDtos())
            {
                if (playerDto.Id == id)
                    return playerDto;
            }
            throw new NullReferenceException();
        }

        public PlayerDto FindPlayerDtoByNick(string playerNick)
        {
            foreach (var playerDto in PlayerDtos)
            {
                if (playerDto.Nick == playerNick)
                    return playerDto;
            }
            throw new NullReferenceException();
        }

        public RequestResourceDto FindRequestResourceDtoById(int id)
        {
            foreach (var requestResourceDto in RequestResourceDtos)
            {
                if (requestResourceDto.Id == id)
                    return requestResourceDto;
            }
            throw new NullReferenceException();
        }

        public FactionDto FindFactionDtoById(int id)
        {
            foreach (var factionDto in FactionDtos)
            {
                if (factionDto.Id == id)
                    return factionDto;
            }
            throw new NullReferenceException();
        }

        /*TODO кэш репы
         * Это как локальный кэш? Нужно ручками делать RefreshPlayers()?
         */
        public IEnumerable<PlayerDto> GetPlayerDtos()
        {
            RefreshPlayers();
            PlayerDtos = _mapper.Map(_players);
            return PlayerDtos;
        }

        public IEnumerable<RequestResourceDto> GetRequestResourceDtos()
        {
            RefreshRequestsResources();
            RequestResourceDtos = _mapper.Map(_requestResources, _players);
            return RequestResourceDtos;
            //return _mapper.Map(_requestResources);
        }

        public IEnumerable<FactionDto> GetFactionDtos()
        {
            RefreshFactions();
            FactionDtos = _mapper.Map(_factions);
            return FactionDtos;
        }

        public void RefreshPlayers()
        {
            if (Connected)
            using (var restClient = this._restClient.CreateRequest())
            {
                //_players = null;
                _players = converter.ConvertJsonToPlayersCollection(restClient.DoGetAsync("player"));
            }
        }

        private void RefreshRequestsResources()
        {
            if (Connected)
                using (var restClient = this._restClient.CreateRequest())
                {
                    //_requestResources = null;
                    _requestResources = converter.ConvertJsonToRequestResourcesCollection(restClient.DoGetAsync("requests/resources"));
                }
        }

        public void RefreshFactions()
        {
            if (Connected)
                using (var restClient = this._restClient.CreateRequest())
            {
                //_factions = null;
                _factions = converter.ConvertJsonToFactionsCollection(restClient.DoGetAsync("faction"));
            }
        }



        /*
        public IEnumerable<PlayerViewModel> GetPlayerViewModels()
        {
            //RefreshPlayers();
            //TestTimer();
            return this.mapper.Map(_players);
        }

        public IEnumerable<RequestResourceViewModel> GetRequestResourceViewModels()
        {
            RefreshRequestsResources();
            RefreshPlayers();
            return this.mapper.Map(requestResources, _players);
        }

        public IEnumerable<PlayerViewModel> PlayerViewModels
        {
            get
            {
                IEnumerable<PlayerViewModel> pvm = new BindingList<PlayerViewModel>();
                //RefreshPlayers();
                //TestTimer();
                
//                this.mapper.Map(_players);
//                foreach (var p in _players)
//                {
//                    var pl = this.mapper.Map(p);
//                    pvm.Add(pl);
//                }
//                return pvm;
                

                return this.mapper.Map(_players);
            }
            set
            {
                RefreshPlayers();
                OnPropertyChanged("PlayerViewModels");
            }
        }

        public IEnumerable<RequestResourceViewModel> RequestResourceViewModels
        {
            get
            {
                //RefreshPlayers();
                //TestTimer();
                return this.mapper.Map(requestResources, _players);
            }
            set
            {
                RefreshRequestsResources();
            }
        }
        */
        void TimeTick(object state)
        {
            //Console.WriteLine("sdf");
            RefreshPlayers();
            //PlayerViewModels = GetPlayerViewModels();

            //RefreshRequestsResources();
            
            GetRequestResourceDtos();
            GetFactionDtos();
        }/**/
    }
}
