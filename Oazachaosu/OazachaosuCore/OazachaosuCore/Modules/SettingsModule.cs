using Autofac;
using Microsoft.Extensions.Configuration;
using OazachaosuCore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Modules
{
    public class SettingsModule : Autofac.Module
    {

        private readonly IConfiguration configuration;

        public SettingsModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var section = typeof(GeneralSettings).Name.Replace("Settings", string.Empty);
            var configurationValue = new GeneralSettings();
            configuration.GetSection(section).Bind(configurationValue);
            builder.RegisterInstance(configurationValue).SingleInstance();
        }

    }
}
