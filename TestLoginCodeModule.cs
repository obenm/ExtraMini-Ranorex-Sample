/*
 * Created by Ranorex
 * User: Admin
 * Date: 9/17/2017
 * Time: 7:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace phar
{
    /// <summary>
    /// Description of UserCodeModule1.
    /// </summary>
    [TestModule("19ADB0B0-7C38-4AA8-A337-EA1DBE3DEC50", ModuleType.UserCode, 1)]
    public class TestLoginCodeModule : ITestModule
    {
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TestLoginCodeModule()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        /// 
       	
       	string _user = "";
       	[TestVariable("45e1de9d-98c2-44fc-9866-0668f258dffb")]
       	public string user
       	{
       		get { return _user; }
       		set { _user = value; }
       	}
       	
       	string _password = "";
       	[TestVariable("76a408ae-02a9-4efd-94c3-ea26fe1a10f8")]
       	public string password
       	{
       		get { return _password; }
       		set { _password = value; }
       	}
       	
       	string _branch = "";
       	[TestVariable("124c4d4f-160e-4c7e-bf88-fac839ce5891")]
       	public string branch
       	{
       		get { return _branch; }
       		set { _branch = value; }
       	}
       	
       	LoginRepository MyRepo = LoginRepository.Instance;
       	Text userField, passwordField, branchField, btnLogin, labelLoading;
        
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            btnLogin = MyRepo.POSAppUWP.Iniciar;
            userField = MyRepo.POSAppUWP.TextUsuario;
            passwordField = MyRepo.POSAppUWP.Contrasena;
            branchField = MyRepo.POSAppUWP.Sucursal;
            labelLoading = MyRepo.POSAppUWP.TextBlock1;

            Delay.Milliseconds(1000);
            
            Report.Start();
            ResetFields();
            
            string[] userList = SplitString(user);
            string[] branchList = SplitString(branch);
            
			Delay.Milliseconds(3000);
           	
			SetFields(userList[0], password, branchList[0]);
            btnLogin.Click();
            
            Delay.Milliseconds(5000); 
            
            if(labelLoading.Visible == true)
            {
            	PrintSuccessReport();
            }
            else
            {
            	PrintAttemptFailed();
            	ResetFields();
            	
            	Delay.Milliseconds(3000); 
            
            	SetFields(userList[1], password, branchList[1]);
	            btnLogin.Click();
	            
	            Delay.Milliseconds(5000);
	             
	            if(labelLoading.Visible == true)
	            {
	            	PrintSuccessReport();
	            }
	            else
	            {
	            	PrintFailedReport();
            		
	            }
            }
        }
        
        private void ResetFields()
        {
    		userField.TextValue = "";
    		passwordField.TextValue = "";
    		branchField.TextValue = "";
        }
        
        private void SetFields(string user, string password, string branch)
        {
    		userField.TextValue = user;
    		passwordField.TextValue = password;
    		branchField.TextValue = branch;
        }
        
        private string[] SplitString(string str)
        {
        	string[] output = str.Split(',');
        	return output;
        }
       	
        private void PrintSuccessReport()
        {
        	Report.Log(Ranorex.ReportLevel.Success, "Login was successful");
	        Report.Success("Success", "You are logged");
        }
        
        private void PrintFailedReport()
        {
        	Report.Log(Ranorex.ReportLevel.Failure, "Login failed");
        	Report.Failure("Failure", "User was not logged");
        }
        
        private void PrintAttemptFailed()
        {
        	Report.Log(Ranorex.ReportLevel.Info, "Attempt Failed");
        	Report.Info("Info", "Attempt to Login Failed!");
        }
        
    }
}
