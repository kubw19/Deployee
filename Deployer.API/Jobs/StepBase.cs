using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Deployer.API.Jobs
{
    public abstract class StepBase : IStepJob
    {

        public string Log { get; set; } = "";
        public DeployPipeContext DeployPipeContext { get; set; }
        public StepBase(DeployPipeContext deployPipeContext)
        {
            DeployPipeContext = deployPipeContext;
        }


        public string DoJob()
        {
            ProcessVariables();
            var body = Body();
            SetOutputVariables();
            return body;
        }

        protected abstract void ProcessVariables();
        protected abstract string Body();
        protected abstract void SetOutputVariables();
        public void RunSSHCommand(SshClient client, string command)
        {
            string res = "";
            try
            {
                res = client.RunCommand(command).Result;
            }
            catch (Exception x)
            {
                res = x.Message;
                Log += res;
                throw;
            }
            Log += res;
        }

        public void RunScp(ScpClient client, Stream stream, string remotePath)
        {
            string res = "";
            try
            {
                client.Upload(stream, remotePath);
            }
            catch (Exception x)
            {
                res = x.Message;
                Log += res;
                throw;
            }
            Log += res;
        }


        protected void ReplaceVariables(Type type, object options)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties().Where(x => x.PropertyType == typeof(string)))
            {
                var value = (string)propertyInfo.GetValue(options);

                foreach (var x in DeployPipeContext.ContextVariables)
                {
                    value = value.Replace($"{{{x.Key}}}", x.Value);
                }
                propertyInfo.SetValue(options, value);
            }
        }
    }




    public class DeployPipeContext
    {
        public int TargetId { get; set; }

        public Dictionary<string, string> ContextVariables = new Dictionary<string, string>();
    }
}
