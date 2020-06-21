using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace InversionOfControl.DependencyInjection
{
    /// <summary>
    /// Registration service
    /// </summary>
    public static class RegistrationService
    {
        #region Fields

        /// <summary>
        /// Recorded service collection
        /// </summary>
        private readonly static IServiceCollection serviceCollection = new ServiceCollection();

        /// <summary>
        /// Service provider
        /// </summary>
        private static ServiceProvider serviceProvider;

        #endregion

        #region Public methods

        /// <summary>
        /// Record a service as a singleton
        /// </summary>
        /// <param name="interfaceToRegister">Type information of interface type to register</param>
        /// <param name="implentationToRegister">Type information of implementation type to register</param>
        public static void RecordAsSingleton(TypeInfo interfaceToRegister, TypeInfo implentationToRegister)
        {
            serviceCollection.AddSingleton(interfaceToRegister, implentationToRegister);
        }

        /// <summary>
        /// Build the service provider with all services recorded
        /// </summary>
        public static void BuildServiceProvider()
        {
            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Get the implementation of the recorded service
        /// </summary>
        /// <param name="param">ParameterInfo of the service interface to get</param>
        /// <returns>Recorded implementation of the service interface to get</returns>
        internal static object Resolve(ParameterInfo param)
        {
            return serviceProvider.GetRequiredService(param.ParameterType);
        }

        #endregion
    }
}
