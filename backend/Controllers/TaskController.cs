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
            string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
            Console.WriteLine(cs);
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            string companyId = "";
            string trainingId = "";
            using (MySqlCommand readCommand = con.CreateCommand())
            {
                readCommand.CommandText = "select company_id from DOCUMENT_MANAGEMENT.company where company_name = @Company_Name";
                readCommand.Parameters.AddWithValue("@Company_Name", model.Company);
                using (var reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        companyId = reader.GetString(0);
                        Console.WriteLine(companyId);
                    }
                }
            }
            using (MySqlCommand readCommand = con.CreateCommand())
            {
                readCommand.CommandText = "select training_id from DOCUMENT_MANAGEMENT.training where training_name = @Training_Name";
                readCommand.Parameters.AddWithValue("@Training_Name", model.Training);
                using (var reader = readCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trainingId = reader.GetString(0);
                        Console.WriteLine(trainingId);
                    }
                }
            }
            using (MySqlCommand readCommand = con.CreateCommand())
            {
                readCommand.CommandText = "select training_index from DOCUMENT_MANAGEMENT.trainingdetails_header where training_id = @Training_Id and company_id = @Company_Id and version = @Version";
                readCommand.Parameters.AddWithValue("Training_Id", trainingId);
                readCommand.Parameters.AddWithValue("Company_Id", companyId);
                readCommand.Parameters.AddWithValue("Version", model.Version);
                using(var reader = readCommand.ExecuteReader())
                {
                  if(reader.HasRows)
                  {
                    return Conflict();
                  }
                }   
            }
            using (MySqlCommand comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO DOCUMENT_MANAGEMENT.trainingdetails_header(Company_ID,Version,Training_ID) VALUES(@Company_ID, @Version,@Training_ID)";
                comm.Parameters.AddWithValue("@Company_ID", companyId);
                comm.Parameters.AddWithValue("@Version", model.Version);
                comm.Parameters.AddWithValue("@Training_ID", trainingId);
                comm.ExecuteNonQuery();
            }

            string trainingIndex ="";
            using (MySqlCommand readCommand = con.CreateCommand())
            {
                readCommand.CommandText = "select training_index from DOCUMENT_MANAGEMENT.trainingdetails_header where training_id = @Training_Id and company_id = @Company_Id and version = @Version";
                readCommand.Parameters.AddWithValue("Training_Id", trainingId);
                readCommand.Parameters.AddWithValue("Company_Id", companyId);
                readCommand.Parameters.AddWithValue("Version", model.Version);
                using(var reader = readCommand.ExecuteReader())
                {
                  if(reader.HasRows)
                  {
                    trainingIndex = reader.GetString(0);
                    Console.WriteLine(trainingIndex);
                  }
                }   
            }

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

            using (MySqlCommand comm = con.CreateCommand())
            {
                comm.CommandText = "INSERT INTO DOCUMENT_MANAGEMENT.trainingdetails_data(Training_Index,Filepath,minimum_version) VALUES(@Training_Index, @File_Path,@Minimum_Version)";
                comm.Parameters.AddWithValue("@Training_Index", trainingIndex);
                comm.Parameters.AddWithValue("@File_Path", path);
                comm.Parameters.AddWithValue("@Minimum_Version", model.MinVersion);
                comm.ExecuteNonQuery();
                con.Close();
            }
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
            currentDirectoryoffiles = currentDirectoryoffiles + @"\wwwroot\files\";
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