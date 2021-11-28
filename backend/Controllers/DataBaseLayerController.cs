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
        var companyNames="";
        while (rdr.Read())
        { 
            Console.WriteLine("{0} {1}", rdr.GetInt32(0), rdr.GetString(1));
            companyNames+=rdr.GetString(1)+"|";
        }    
        Console.WriteLine(companyNames);
        
        return companyNames;
    }
    [HttpGet]
  [Route("training")]
    public string GetTraining(){
        string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
        using var con = new MySqlConnection(cs);
        con.Open();
        var trainingNames="";
        var CommandText = "SELECT * FROM DOCUMENT_MANAGEMENT.Training";
        using var cmd = new MySqlCommand(CommandText, con);
        using MySqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        { 
            Console.WriteLine("{0} {1}", rdr.GetInt32(0), rdr.GetString(1));
            trainingNames+=rdr.GetString(1)+"|";
        }    
        Console.WriteLine(trainingNames);
        return trainingNames;
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

 

[HttpGet]
  [Route("getFilteredData")]
    public void GetFilteredList(string filterParameters){
       Console.WriteLine(filterParameters);
        var userDetails = filterParameters.Split("|");
        
        var emptyVersion="undefined";
        string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
        using var con = new MySqlConnection(cs);
        con.Open();
        var Company_ID="SELECT company.COMPANY_ID FROM DOCUMENT_MANAGEMENT.company WHERE company.COMPANY_NAME="+@""""+userDetails[0]+@"""";
        Console.WriteLine(Company_ID);
        var Training_ID="SELECT training.Training_ID FROM DOCUMENT_MANAGEMENT.training WHERE training.TRAINING_NAME="+@""""+userDetails[2]+@"""";
        Console.WriteLine(Training_ID);
        using var cmd = new MySqlCommand(Company_ID, con);
        
        using var cmd2=new MySqlCommand(Training_ID,con);

         using MySqlDataReader rdr1 = cmd.ExecuteReader();
    
        while (rdr1.Read())
        { 
              
            userDetails[0]=rdr1.GetInt32(0).ToString();
            
        }   
        con.Close();
        con.Open();
        Console.WriteLine(userDetails[0]);
         using MySqlDataReader rdr2 = cmd2.ExecuteReader();
    
        while (rdr2.Read())
        { 
              
            userDetails[2]=rdr2.GetInt32(0).ToString();
            
        }  
        con.Close();
        Console.WriteLine(userDetails[2]);

      
        var CommandText=@"SELECT * FROM DOCUMENT_MANAGEMENT.trainingdetails_header INNER JOIN DOCUMENT_MANAGEMENT.trainingdetails_data ON trainingdetails_header.Training_index=trainingdetails_data.Training_index" ;
        
         if((userDetails[0]=="ALL") && (userDetails[1]==emptyVersion) && (userDetails[2]=="ALL") )
         {
         CommandText =CommandText;
        }
         else if((userDetails[0]=="ALL") && (userDetails[1]==emptyVersion) && (userDetails[2]!="ALL")  )
         {
                CommandText = CommandText+" WHERE trainingdetails_header.Training_ID="+userDetails[2];


         }
          else if((userDetails[0]=="ALL") && (userDetails[1]!=emptyVersion) && (userDetails[2]=="ALL")  )
         {
             CommandText = CommandText+" WHERE trainingdetails_header.Version="+userDetails[2];
         }
          else if((userDetails[0]=="ALL") && (userDetails[1]!=emptyVersion) && (userDetails[2]!="ALL")  )
         {
              CommandText = CommandText+" WHERE trainingdetails_header.Version="+userDetails[1]+" AND trainingdetails_header.Training_ID= "+userDetails[2];
         }
          else if((userDetails[0]!="ALL") && (userDetails[1]==emptyVersion) && (userDetails[2]=="ALL")  )
         {
                CommandText = CommandText+" WHERE trainingdetails_header.Company_ID="+userDetails[0];
         }
          else if((userDetails[0]!="ALL") && (userDetails[1]==emptyVersion) && (userDetails[2]!="ALL")  )
         {
 CommandText = CommandText+" WHERE trainingdetails_header.Company_ID="+userDetails[0]+" AND trainingdetails_header.Training_ID= "+userDetails[2];
    

         }
         
          else if((userDetails[0]!="ALL") && (userDetails[1]!=emptyVersion) && (userDetails[2]=="ALL")  )
         {
             CommandText = CommandText+" WHERE trainingdetails_header.Company_ID="+userDetails[0]+" AND trainingdetails_header.Version= "+userDetails[1];
  
         }
         
        else{
 CommandText = CommandText+" WHERE trainingdetails_header.Company_ID="+userDetails[0]+" AND trainingdetails_header.Version= "+userDetails[1]+" AND trainingdetails_header.Training_ID="+userDetails[2];
  
        }
            Console.WriteLine(CommandText);
            con.Open();
        using var cmd3 = new MySqlCommand(CommandText, con);
        using MySqlDataReader rdr = cmd3.ExecuteReader();
        while(rdr.Read())
        {
                Console.WriteLine("hiii");
                Console.WriteLine("{0} {1} {2} {3} {4}  ", rdr.GetInt32(1),
                rdr.GetString(2),rdr.GetInt32(3),rdr.GetString(6),rdr.GetString(7));
        }
    con.Close();
        
    }


[HttpGet]
  [Route("getFileCheck")]
    public bool GetFileCheck(){
        Console.WriteLine("file check in database");
        var status=true;
        return status;
    }
 }
}