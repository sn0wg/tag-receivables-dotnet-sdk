using System;
using System.Collections.Generic;
using TagSDK.Services;
using Microsoft.Extensions.DependencyInjection;
using TagSDK.Services.Interfaces;
using TagSDK.Pipeline.Interfaces;
using TagSDK.Pipeline;
using TagSDK.Validators;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using TagSDK.Authorization;
using TagSDK.Services.Receivable.Advancement;
using TagSDK.Services.Receivable.Consent;
using TagSDK.Services.Receivable.Contract;
using TagSDK.Services.Receivable.Notification;
using TagSDK.Services.Receivable.Position;
using TagSDK.Services.Receivable.Reconciliation;
using TagSDK.Services.Receivable.Register;
using TagSDK.Services.Receivable.Settlement;
using TagSDK.Services.Receivable.Transaction;

namespace TagSDK.Factories
{
    public static class TagSdk
    {
        private static TagServiceCollection factory = null;
        public static TagServiceCollection GetServices(Action<SDKOptions> options)
        {
            if (factory == null)
            {
                IServiceCollection serviceCollection = new ServiceCollection();

                SDKOptions _options = new SDKOptions();
                options(_options);

                RegisterServices(serviceCollection, _options);

                factory = new TagServiceCollection(serviceCollection.BuildServiceProvider());
            }

            return factory;
        }

        public static IServiceCollection AddTagSDK(this IServiceCollection services, Action<SDKOptions> options)
        {
            SDKOptions _options = new SDKOptions();
            options(_options);

            RegisterServices(services, _options);

            return services;
        }

        private static void RegisterServices(IServiceCollection services, SDKOptions options)
        {
            services
                        .AddSingleton<SDKOptions>(options)
                        .AddTransient<IModelValidator, DefaultModelValidator>()
                        .AddTransient<IRestClient, RestClient>((provider) =>
                        {
                            RestClient restClient = new RestClient();
                            restClient.UseNewtonsoftJson();
                            return restClient;
                        })
                        .AddSingleton<IAuthorizator, DefaultAuthorizator>()
                        .AddTransient(typeof(LogMidleware<,>))
                        .AddTransient(typeof(ValidateMidleware<,>))
                        .AddTransient(typeof(RequestMidleware<,>))
                        .AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineDefaultBehavior<,>))
                        //services
                        .AddTransient<IAuthenticateService, AuthenticateService>()
                        .AddTransient<IReceivableService, ReceivableService>()
                        .AddTransient<IAdvancementService, AdvancementService>()
                        .AddTransient<ICommercialEstablishmentService, CommercialEstablishmentService>()
                        .AddTransient<INotificationService, NotificationService>()
                        .AddTransient<ITransactionService, TransactionService>()
                        .AddTransient<ISettlementService, SettlementService>()
                        .AddTransient<IPositionService, PositionService>()
                        .AddTransient<IContractService, ContractService>()
                        .AddTransient<IContractService, ContractService>()
                        .AddTransient<IConsentService, ConsentService>()
                        .AddTransient<IReconciliationService, ReconciliationService>();
        }
    }

    public class TagServiceCollection
    {
        protected readonly IServiceProvider serviceProvider;

        internal TagServiceCollection(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            FluentValidation.AssemblyScanner
                .FindValidatorsInAssembly(typeof(TagServiceCollection).Assembly)
                .ForEach(result =>
                {
                    if (Validators.ContainsKey(result.InterfaceType))
                        (Validators[result.InterfaceType] as List<Type>).Add(result.ValidatorType);
                    else
                        Validators.Add(result.InterfaceType, new List<Type>() { result.ValidatorType });
                });
        }

        private Dictionary<Type, IEnumerable<Type>> Validators { get; set; } = new Dictionary<Type, IEnumerable<Type>>();

        public IAuthenticateService AuthenticateService { get => serviceProvider.GetService<IAuthenticateService>(); }

        public IReceivableService ReceivableService { get => serviceProvider.GetService<IReceivableService>(); }

        public IAdvancementService AdvancementService { get => serviceProvider.GetService<IAdvancementService>(); }

        public ICommercialEstablishmentService CommercialEstablishmentService { get => serviceProvider.GetService<ICommercialEstablishmentService>(); }

        public INotificationService NotificationService { get => serviceProvider.GetService<INotificationService>(); }

        public ITransactionService TransactionService { get => serviceProvider.GetService<ITransactionService>(); }

        public ISettlementService SettlementService { get => serviceProvider.GetService<ISettlementService>(); }

        public IReconciliationService ReconciliationService { get => serviceProvider.GetService<IReconciliationService>(); }
        public IPositionService PositionService { get => serviceProvider.GetService<IPositionService>(); }

        public IContractService ContractService { get => serviceProvider.GetService<IContractService>(); }

        public IConsentService ConsentService { get => serviceProvider.GetService<IConsentService>(); }

    }
}
