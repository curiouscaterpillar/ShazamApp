using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.QuickInject;
using ShazamApp.Application;
using ShazamApp.Core;
using ShazamApp.Core.Abstractions;
using ShazamApp.DataAccess;
using ShazamApp.Entities;
using ShazamApp.Handlers;

namespace ShazamApp.Startup
{
    public class DependencyBootstrapper
    {
        private static QuickInjectContainer container;

        public static QuickInjectContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = Configure();
                }

                return container;
            }
        }

        private static QuickInjectContainer Configure()
        {
            var container = new QuickInjectContainer();

            container.RegisterType<IRecordContext<Record>, ShazamContext>(new TransientLifetimeManager());
            container.RegisterType<IRecordService, RecordService>(new TransientLifetimeManager());
            container.RegisterType<SaveEndpointHandler>();
            container.RegisterType<GetRecordsEndpointHandler>();

            return container;
        }
    }
}
