using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.Text;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.WebService;

namespace RepositoryCommunityHelper.Repository
{
    public class Repository : BaseClass, IRepository
    {
        private readonly IService _restClient;
        private readonly IMapper _mapper;

        private IEnumerable<Player> players { get; set; }
        private IEnumerable<RequestResource> _requestResources { get; set; }




        //private TimerCallback timeCB;
        //private Timer Timer1;

        public Repository(IService restClient, IMapper mapper)
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
            players = new ObservableCollection<Player>();
            _requestResources = new ObservableCollection<RequestResource>();

            //timeCB = new TimerCallback(TimeTick);

            //Timer1 = new Timer(timeCB, null, 10000, 10000);

        }

        /*TODO 
         * Это как локальный кэш? Нужно ручками делать RefreshPlayers()?
         */
        public IEnumerable<PlayerDto> GetPlayerDtos()
        {
            //RefreshPlayers();
            return _mapper.Map(players);
        }

        public IEnumerable<RequestResourceDto> GetRequestResourceDtos()
        {
            return _mapper.Map(_requestResources);
        }

        public void RefreshPlayers()
        {
            using (var restClient = this._restClient.CreateRequest())
            {
                players = ConvertJsonToPlayers(restClient.DoGetAsync("player").Result);
            }
        }

        public void RefreshRequestsResources()
        {
            using (var restClient = this._restClient.CreateRequest())
            {
                _requestResources = ConvertJsonToRequestResources(restClient.DoGetAsync("requests/resources").Result);
                //var players = convertPlayers(baseService.restClient.DoGetAsync("player").Result);
                //var players = convertPlayers(baseService.PlayerService.GetAllPlayers());
                //return this.mapper.Map(requestsResources, players);
            }
        }


        public IEnumerable<RequestResource> ConvertJsonToRequestResources(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<RequestResource>));
            List<RequestResource> clear = (List<RequestResource>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public IEnumerable<Player> ConvertJsonToPlayers(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Player>));
            List<Player> clear = (List<Player>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }



        /*
        public IEnumerable<PlayerViewModel> GetPlayerViewModels()
        {
            //RefreshPlayers();
            //TestTimer();
            return this.mapper.Map(players);
        }

        public IEnumerable<RequestResourceViewModel> GetRequestResourceViewModels()
        {
            RefreshRequestsResources();
            RefreshPlayers();
            return this.mapper.Map(requestResources, players);
        }

        public IEnumerable<PlayerViewModel> PlayerViewModels
        {
            get
            {
                IEnumerable<PlayerViewModel> pvm = new BindingList<PlayerViewModel>();
                //RefreshPlayers();
                //TestTimer();
                
//                this.mapper.Map(players);
//                foreach (var p in players)
//                {
//                    var pl = this.mapper.Map(p);
//                    pvm.Add(pl);
//                }
//                return pvm;
                

                return this.mapper.Map(players);
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
                return this.mapper.Map(requestResources, players);
            }
            set
            {
                RefreshRequestsResources();
            }
        }

        void TimeTick(object state)
        {
            //Console.WriteLine("sdf");
            RefreshPlayers();
            //PlayerViewModels = GetPlayerViewModels();

            RefreshRequestsResources();
        }*/
    }
}
