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


 public class DataBaseLayerController : ControllerBase
 {
  IWebHostEnvironment environment;
  public DataBaseLayerController(IWebHostEnvironment environment)
  {
   this.environment = environment;
  }

public MySqlConnection unv(){
             string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
            Console.WriteLine(cs);

            using var con = new MySqlConnection(cs);
            con.Open();

            Console.WriteLine($"MySQL version : {con.ServerVersion}");
            var cmd = new MySqlCommand();
        cmd.Connection = con;
    
    return con;
}
[HttpGet]
  [Route("company")]
public string GetCompany(){
        string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
        using var con = new MySqlConnection(cs);
        con.Open();
        var CommandText = "SELECT * FROM DOCUMENT_MANAGEMENT.Company";
        using var cmd = new MySqlCommand(CommandText, con);
        using MySqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        { 
            Console.WriteLine("{0} {1}", rdr.GetInt32(0), rdr.GetString(1));
        }    
        return "";
    }
    [HttpGet]
  [Route("training")]
    public string GetTraining(){
        string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
        using var con = new MySqlConnection(cs);
        con.Open();
        var CommandText = "SELECT * FROM DOCUMENT_MANAGEMENT.Training";
        using var cmd = new MySqlCommand(CommandText, con);
        using MySqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        { 
            Console.WriteLine("{0} {1}", rdr.GetInt32(0), rdr.GetString(1));
        }    
        return "";
    }
  [HttpGet]
  [Route("authenticate")]
    public bool GetValidUser(string credentials){
       
        var userDetails = credentials.Split("|");
        string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
        using var con = new MySqlConnection(cs);
        con.Open();
        var CommandText = @"SELECT COUNT(*) FROM DOCUMENT_MANAGEMENT.UserCredentials WHERE UserCredentials.USER_NAME="""+userDetails[0]+@""" and UserCredentials.PASSWORD="""+userDetails[1]+@"""" ;
        Console.WriteLine(CommandText);
        using var cmd = new MySqlCommand(CommandText, con);
        using MySqlDataReader rdr = cmd.ExecuteReader();
        var count=0;
        while (rdr.Read())
        { 
              
            count=rdr.GetInt32(0);
            Console.WriteLine(count);
        }    
        if(count>=1)
        {
            return true;
        }
        else{
                return false;
        }
        
    }
 }
}