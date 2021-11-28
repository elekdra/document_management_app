using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace backend.Controllers
{
 [ApiController]
 [Route("api/[controller]")]


 public class TaskController : ControllerBase
 {
  IWebHostEnvironment environment;
  public TaskController(IWebHostEnvironment environment)
  {
   this.environment = environment;
  }
  [HttpGet]
  [Route("authenticate")]
  public bool UserAuthenticate(string credentials)
  {
   var userDetails = credentials.Split("|");
   Console.WriteLine(credentials);
   var currentDirectory = System.IO.Directory.GetCurrentDirectory();
   string jsonPath = currentDirectory + @"\wwwroot\Credentials\Credentials.json";
   var userCredentials = System.IO.File.ReadAllText(jsonPath);
   object jsonObject = JsonConvert.DeserializeObject(userCredentials);
   var list = JsonConvert.DeserializeObject<List<userCredentials>>(userCredentials);
   bool Flag = false;
   foreach (var item in list)
   {
    if (item.userName == userDetails[0] && item.passWord == userDetails[1])
    {
     Flag = true;
    }
   }
   if (Flag)
   {
    return true;
   }
   else
   {
    return false;
   }
  }


  [HttpPut]
  [Route("filesave")]
  public IActionResult PutFileNames([FromBody] FileModel model)
  {
   Console.WriteLine(model.Mode);
   string webRootPath = environment.WebRootPath;
   string filesPath = Path.Combine(webRootPath, "files");
   string fileNamesave = $"{Path.GetFileNameWithoutExtension(model.FileName)}_{Guid.NewGuid().ToString()}{Path.GetExtension(model.FileName)}";
   string path = filesPath + "\\" + fileNamesave;
   String[] cnts = model.FileContent.Split("base64,");
   byte[] data = Convert.FromBase64String(cnts[1]);
   using (System.IO.FileStream stream = System.IO.File.Create(path))
   {
    stream.Write(data, 0, data.Length);
   }
 //database update
 string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
            Console.WriteLine(cs);

            using var con = new MySqlConnection(cs);
            con.Open();
            MySqlCommand comm = con.CreateCommand();
            comm.CommandText = "INSERT INTO DOCUMENT_MANAGEMENT.trainingdetails_header(Company_ID,Version,Training_ID) VALUES(@Company_ID, @Version,@Training_ID)";
            comm.Parameters.AddWithValue("@Company_ID", "103");
            comm.Parameters.AddWithValue("@Version", "6.9");
            comm.Parameters.AddWithValue("@Training_ID", "1003");
            
            comm.ExecuteNonQuery();
            con.Close();

   return Ok();
  }




  [HttpGet()]
  [Route("filedelete")]
  public String GetFileDelete(string file)
  {
   
   string[] fileProperties = file.Split('|');
   string json = JsonFilePath();
   var jsonString = System.IO.File.ReadAllText(json);
   object jsonObject = JsonConvert.DeserializeObject(jsonString);
   var list = JsonConvert.DeserializeObject<List<FileModel>>(jsonString);
   FileModel deletedItem = null;
   foreach (var listItem in list)
   {
    if (listItem.FileName == fileProperties[0] && listItem.Version == fileProperties[1] && listItem.Company == fileProperties[2])
    {
     deletedItem = listItem;
    }
   }
   list.Remove(deletedItem);
   var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
   System.IO.File.WriteAllText(json, convertedJson);
   var currentDirectoryoffiles = System.IO.Directory.GetCurrentDirectory();
   currentDirectoryoffiles=currentDirectoryoffiles+ @"\wwwroot\files\";
   string[] Files = Directory.GetFiles(currentDirectoryoffiles);
   string fileName = currentDirectoryoffiles + fileProperties[0];
   if (System.IO.File.Exists(fileName))
   {
    System.IO.File.Delete(fileName);
   }
   Console.WriteLine(file);
   Console.WriteLine(fileName);
   return "File data deleted successfully";
  }

  [HttpGet]
  [Route("getdefaults")]

  public object GetDefaults(string initialize)
  {
   string json = JsonFilePath();
   var jsonString = System.IO.File.ReadAllText(json);
   object jsonObject = JsonConvert.DeserializeObject(jsonString);
   return jsonString;
  }

  public string JsonFilePath()
  {


   var currentDirectory = System.IO.Directory.GetCurrentDirectory();
   string jsonPath = currentDirectory + @"\wwwroot\ConfigData\data.json";
   Console.WriteLine(jsonPath);
   return jsonPath;
  }

 }
}