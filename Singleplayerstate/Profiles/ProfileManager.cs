
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Singleplayerstate.Profiles
{
    public class ProfileManager
    {
        private static ProfileManager Instance;

        private string ProfilePath = Path.Combine(AppContext.BaseDirectory, "profiles");
        private string CurrentSptPath;

        public static ProfileManager Get()
        {
            if (Instance == null) Instance = new ProfileManager();
            return Instance;
        }
        public static void Dispose() => Instance = null;
        
        private ProfileManager()
        {
            if (!Directory.Exists(ProfilePath)) Directory.CreateDirectory(ProfilePath);
        }       

        public void SetCurrentPath(string sptPath)
        {
            Console.WriteLine("SET PATH");
            CurrentSptPath = Path.Combine(sptPath, "user", "profiles");
            GetProfiles();
        }

        public void GetProfiles()
        {
            Console.WriteLine("GET PROFILES");
            if (String.IsNullOrEmpty(CurrentSptPath)) return;

            string[] files = Directory.GetFiles(ProfilePath);
            foreach (string file in files)
            {
                if (!file.EndsWith(".json")) continue;
                string filename = Path.GetFileName(file);
                string sptFile = Path.Combine(CurrentSptPath, filename);
                try
                {
                    FileInfo lf = new FileInfo(file);
                    if (File.Exists(sptFile)) lf.Replace(sptFile, $"{sptFile}.backup");
                    else lf.CopyTo(sptFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void SaveProfiles()
        {
            Console.WriteLine("SAVE PROFILES");
            if (String.IsNullOrEmpty(CurrentSptPath)) return;

            string[] files = Directory.GetFiles(CurrentSptPath);
            foreach (string file in files)
            {
                if (!file.EndsWith(".json")) continue;
                string filename = Path.GetFileName(file);
                string launcherFile = Path.Combine(ProfilePath, filename);
                try
                {
                    FileInfo lf = new FileInfo(file);
                    if (File.Exists(launcherFile)) lf.Replace(launcherFile, $"{launcherFile}.backup");
                    else lf.CopyTo(launcherFile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
