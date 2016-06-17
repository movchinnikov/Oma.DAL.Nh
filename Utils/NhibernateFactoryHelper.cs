namespace Oma.DAL.Nh.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using NHibernate;

    public class NhibernateFactoryHelper
    {
        public virtual ISessionFactory CreateFactory()
        {
            return Fluently.Configure()
                .Database(this.Configurer)
                .Mappings(m =>
                {
                    var autoPersistenModel = AutoMap.Assemblies(this.GetAutomappingConfiguration,
                        this.SetAssemblyWithEntities());

                    foreach (var type in this.SetIgnoreBase())
                    {
                        autoPersistenModel.IgnoreBase(type);
                    }

                    autoPersistenModel
                        .Conventions.Add(this.SetConvention())
                        .UseOverridesFromAssembly(this.GetType().Assembly);

                    foreach (var assembly in this.SetAssemblyWithEntities())
                    {
                        autoPersistenModel.UseOverridesFromAssembly(assembly);
                    }

                    m.AutoMappings.Add(autoPersistenModel);
                })
                .Mappings(m => m.HbmMappings.AddFromAssembly(this.GetType().Assembly))

#if DEBUG
                .ExposeConfiguration(x => x.SetInterceptor(new SqlStatementInterceptor()))
#endif
                .BuildSessionFactory();
        }

        protected virtual IAutomappingConfiguration GetAutomappingConfiguration
        {
            get { return new AutomappingConfiguration(); }
        }

        protected virtual IEnumerable<Assembly> SetAssemblyWithEntities() { return new Assembly[0]; }

        protected virtual IEnumerable<Type> SetIgnoreBase() { return new Type[0]; }

        protected virtual Type SetConvention() { return typeof(MainConvention); }

        protected virtual string ConnectionString { get { return ""; } }

        protected virtual IPersistenceConfigurer Configurer
        {
            get
            {
                return MsSqlConfiguration.MsSql2008
                    .ConnectionString(this.ConnectionString);
            }
        }
    }
}