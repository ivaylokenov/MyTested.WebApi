namespace SourceControlSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Api.Infrastructure.Mapping;
    using AutoMapper;

    public static class AutoMapperConfig
    {
        private static TypeInfo ProfileTypeInfo = typeof(Profile).GetTypeInfo();

        public static void RegisterMappings(params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.GetExportedTypes().Select(t => t.GetTypeInfo()));

            var profiles = allTypes
                .Where(t => ProfileTypeInfo.IsAssignableFrom(t))
                .Where(t => !t.IsAbstract);

            Mapper.Initialize(configuration =>
            {
                configuration.AddProfile(StandardMappingsProfile.From(allTypes));
                configuration.AddProfile(CustomMappingsProfile.From(allTypes));

                foreach (var profile in profiles)
                {
                    configuration.AddProfile(profile);
                }
            });
        }
        
        private class StandardMappingsProfile : Profile
        {
            private static Type MapFromType = typeof(IMapFrom<>);

            private readonly IEnumerable<TypeInfo> types;

            public static StandardMappingsProfile From(IEnumerable<TypeInfo> types)
                => new StandardMappingsProfile(types);

            private StandardMappingsProfile(IEnumerable<TypeInfo> types)
            {
                this.types = types;
                this.LoadMappings();
            }

            private void LoadMappings()
            {
                var maps = this.types
                    .SelectMany(
                        t => t.GetInterfaces(),
                        (t, i) => new
                        {
                            Type = t,
                            Interface = i.GetTypeInfo()
                        })
                    .Where(
                        map =>
                            map.Interface.IsGenericType
                            && map.Interface.GetGenericTypeDefinition() == MapFromType
                            && !map.Type.IsAbstract
                            && !map.Type.IsInterface)
                    .Select(type => new
                    {
                        Source = type.Interface.GetGenericArguments()[0],
                        Destination = type.Type.AsType()
                    });

                foreach (var map in maps)
                {
                    CreateMap(map.Source, map.Destination);
                    CreateMap(map.Destination, map.Source);
                }
            }
        }

        private class CustomMappingsProfile : Profile
        {
            private static TypeInfo CustomMappingsTypeInfo = typeof(IHaveCustomMappings).GetTypeInfo();

            private readonly IEnumerable<TypeInfo> types;

            public static CustomMappingsProfile From(IEnumerable<TypeInfo> types)
                => new CustomMappingsProfile(types);

            private CustomMappingsProfile(IEnumerable<TypeInfo> types)
            {
                this.types = types;
                this.LoadMappings();
            }

            private void LoadMappings()
            {
                var maps = this.types
                    .SelectMany(
                        t => t.GetInterfaces(),
                        (t, i) => new
                        {
                            Type = t,
                            Interface = i.GetTypeInfo()
                        })
                    .Where(
                        map =>
                            CustomMappingsTypeInfo.IsAssignableFrom(map.Type)
                            && !map.Type.IsAbstract
                            && !map.Type.IsInterface)
                    .Select(type => (IHaveCustomMappings)Activator.CreateInstance(type.Type.AsType()));

                foreach (var map in maps)
                {
                    map.CreateMappings(this);
                }
            }
        }
    }
}
