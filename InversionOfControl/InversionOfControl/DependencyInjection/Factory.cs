using System.Collections.Generic;

namespace InversionOfControl.DependencyInjection
{
    /// <summary>
    /// Object factory
    /// </summary>
    public static class Factory
    {
        #region Public methods

        /// <summary>
        /// Construct an object
        /// </summary>
        /// <typeparam name="T">Type of the object to construct</typeparam>
        /// <returns>An instance of the object of type T</returns>
        /// <remarks>All parameters of the constructor of type must be register in the service registry <see cref="RegistrationService"/></remarks>
        /// <remarks>The object T must have only one constructor</remarks>
        public static T Construct<T>()
        {
            var allConstructors = typeof(T).GetConstructors();
            var firstConstructor = allConstructors[0];
            var paramList = new List<object>();
            foreach (var param in firstConstructor.GetParameters())
            {
                var service = RegistrationService.Resolve(param);
                paramList.Add(service);
            }

            return (T)firstConstructor.Invoke(paramList.ToArray());
        }

        #endregion
    }
}
