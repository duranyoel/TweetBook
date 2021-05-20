using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Installers
{
    public interface IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration);

    }
}
