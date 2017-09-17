/////////////////////////////////////////////////////////////////////////////////////
DockerAZ-14-ASPNETCore
ASPNETCoreExemplo
/////////////////////////////////////////////////////////////////////////////////////
@{
    ViewData["Title"] = "About";
}
<h2>@ViewData["Title"]</h2>
<h3>@ViewData["Message"]</h3>

<p><strong>CommandLine:</strong> @System.Environment.CommandLine</p>
<p><strong>CurrentDirectory:</strong> @System.Environment.CurrentDirectory</p>
<p><strong>OSVersion:</strong> @System.Environment.OSVersion</p>
<p><strong>MachineName:</strong> @System.Environment.MachineName</p>
<p><strong>UserName:</strong> @System.Environment.UserName</p>
/////////////////////////////////////////////////////////////////////////////////////
Nuget MongoDB.Driver
/////////////////////////////////////////////////////////////////////////////////////
    networks:
      - demo_aspnet

  meu_mongo:
    image: mongo:3.4
#    ports:
#      - "27017:27017"
    volumes:
      - db_mongo:/data/db
    networks:
      - demo_aspnet
    restart: always
    command: --storageEngine wiredTiger
    environment: 
      MONGO_INITDB_ROOT_USERNAME: mongouser
      MONGO_INITDB_ROOT_PASSWORD: GPX4WOwpcvOc9Wm70gAG8It7tKA0Cy090ZVO82cEJsExogsMDY



volumes:
  db_mongo:


networks: 
  demo_aspnet: 
    driver: bridge

/////////////////////////////////////////////////////////////////////////////////////
public class Aluno
{
	public MongoDB.Bson.ObjectId _id { get; set; }

	public string Nome { get; set; }

	public int Idade { get; set; }
}
/////////////////////////////////////////////////////////////////////////////////////
"Connections": {
	"MongoDB": "mongodb://mongouser:GPX4WOwpcvOc9Wm70gAG8It7tKA0Cy090ZVO82cEJsExogsMDY@meu_mongo:27017/admin"
}
/////////////////////////////////////////////////////////////////////////////////////
services.AddScoped((sp) =>
{
	var mongoDbConnectionString = this.Configuration.GetSection("Connections").GetValue<string>("MongoDB");
	return new MongoDB.Driver.MongoClient(mongoDbConnectionString);
});
/////////////////////////////////////////////////////////////////////////////////////
public IActionResult Add()
{
	var collection = mongoDbClient.GetDatabase("admin").GetCollection<Aluno>("aluno");
	collection.InsertMany(new[] {
		new Aluno() { Nome = "Aluno1", Idade = 17 },
		new Aluno() { Nome = "Aluno2", Idade = 18 }
	});
	return Ok("2 alunos adicionados");
}
/////////////////////////////////////////////////////////////////////////////////////
/home/add
/////////////////////////////////////////////////////////////////////////////////////
var listaAluno = mongoDbClient.GetDatabase("admin").GetCollection<Aluno>("aluno").Find(Builders<Aluno>.Filter.Where(it => true)).ToList();
/////////////////////////////////////////////////////////////////////////////////////
@Model List<Aluno>

<h1>Alunos</h1>
@foreach(var aluno in Model){ 

    <p>@aluno.Nome / @aluno.Idade </p>
}
/////////////////////////////////////////////////////////////////////////////////////

<p style="color:red"><strong>Platform:</strong> @System.Environment.OSVersion.Platform</p>
<p style="color:red"><strong>VersionString:</strong> @System.Environment.OSVersion.VersionString</p>


node{
    stage('Checkout'){
        git 'https://github.com/luizcarlosfaria/DockerAZ-14-ASPNETCore.git'
    }
    stage('build'){
        sh 'sudo docker-compose -f docker-compose.ci.build.yml up'
        sh 'sudo docker-compose -f docker-compose.ci.build.yml down --remove-orphans'
    }
    stage('deploy'){
        sh 'sudo docker-compose down --remove-orphans'
        sh 'sudo docker-compose up -d --build'
        sh 'sudo docker ps -a | grep aspnetcoreexemplo'
    }
}