This is the backend 

Created with c# and mongoDB 

Basic Get, Post and Put operations using .net api app framework and connecting it to the mongoDB cloud servers 

 model for our database what is a poll below is our model
 
        public class EpollModel
    
    {
        //get MONGODB
        [BsonId] //mongodb id = primarykey
        //assets key
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; }
        public string Title { get; set; }

        public List<OptionModel> Options { set; get; }
    }
    public class OptionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Counts { get; set; }
    }
  creating our model like this and nesting list func inside the epollmodel we can create similar stucture as in the javascript objects
this has been hidden in the dotnet user-secrets list :     
CONNECTION_STRING = mongodb+srv://tom:123@cluster0.vttgrnm.mongodb.net/EpollDb?retryWrites=true&w=majority

in order to do the same use nuget packet manager console to 

init the user-secrets -p {proj name}
then add CONNECTION_STRING = mongodb+srv://tom:123@cluster0.vttgrnm.mongodb.net/EpollDb?retryWrites=true&w=majority
this prevents valuable keys to be published e.g. github


  In the package.json : include 
         
          "environmentVariables": {
        "DATABASE_NAME": "EpollDb",
        "EPOLL_COLLECTION_NAME": "Epoll"
      }
      
   this is same as our dbconfig file
   
    {
      public class EpollDbConfig
        //same format as in launchsettings.json
        public string Database_Name { get; set; }
        public string Epoll_Collection_Name { get; set; }
        public string Connection_String { get; set; } //hidden in the secrets
    }
    
    
    {

Next is to create client for the mongoDb connection 

interface we need for DbClient
      
     public interface IDbClient 
     {
         IMongoCollection<EpollModel> GetEpollCollection();
    }

        public class DbClient : IDbClient
    {
        private readonly IMongoCollection<EpollModel> _epoll;
        //constructor to inject options and inject EpollDbConfig and name it same
        public DbClient(IOptions<EpollDbConfig> epollDbConfig)
        {
            //get our db and collection 
            var client = new MongoClient(epollDbConfig.Value.Connection_String);
            //get dabase we get from mongodb
            var database = client.GetDatabase(epollDbConfig.Value.Database_Name);

            _epoll = database.GetCollection<EpollModel>(epollDbConfig.Value.Epoll_Collection_Name);


        }


        //this from IDbclient interface
        public IMongoCollection<EpollModel> GetEpollCollection() => _epoll; //implement the code by returning
    }
           
  
   ApiController file :  handles the endpoints  and looks like this 
         
    {
    [ApiController]
    [Route("[controller]")]
    public class PollsController : ControllerBase
    {
        private readonly IEpollServices _epollServices;
        //injection
        public PollsController(IEpollServices epollServices)
        {
            _epollServices = epollServices;
        }

        [HttpGet]
        public IActionResult GetEpolls()
        {
            return Ok(_epollServices.GetEpolls());
        }

        //create new poll
        
        [HttpPost("add/")]
        public IActionResult AddEpoll(EpollModel epollModel)
        {
            //return 
            _epollServices.AddEpoll(epollModel);
            //201 resp and return id matching the poll
            return CreatedAtRoute("GetEpoll", new { id = epollModel.Id }, epollModel);
        }
        
  
 e.g http post gets called Interface IEpoll service comes to play
 
 
      public interface IEpollServices
 
    {
       
        EpollModel AddEpoll(EpollModel epollModel);
   
       
    }
      
  then it passes to our services file and have actions for corresponding requests 
    
   
   
         public class EpollServices : IEpollServices //implement interfaces 
  
    {
        //this collection
        private readonly IMongoCollection<EpollModel> _epollModel;
        public EpollServices(IDbClient dbClient)
        {
          _epollModel =  dbClient.GetEpollCollection();
                   
        }   
   
    //create
        public EpollModel AddEpoll(EpollModel epollModel)
        {
            _epollModel.InsertOne(epollModel);
            return epollModel;
        }
  
  As we can see backend works from Controller to IEpollservices to Epollservies whilst using the dbclient etc.
  
  In the startup we need to set some configurations because of corspolicy and dependency injections
  
     public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

  // This method gets called by the runtime. Use this method to add services to the container.
  //need to add singleton This allows us to inject ind 
  
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDbClient,DbClient>(); 
            services.Configure<EpollDbConfig>(Configuration); //config
            services.AddTransient<IEpollServices, EpollServices>();
            services.AddCors();
            services.AddControllers();
           
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Epoll.WebApp", Version = "v1" });
            });
        }

   This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
   
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Epoll.WebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
global cors policy

              app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
               app.UseAuthorization();

           
    
