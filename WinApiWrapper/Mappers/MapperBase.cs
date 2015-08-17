using System.Collections.Generic;


namespace WinApiWrapper.Mappers
{
    public abstract class MapperBase<TFrom, TTo> where TFrom : struct where TTo : class
    {
        protected Dictionary<TFrom, TTo> Mappings = new Dictionary<TFrom, TTo>();

        public TTo Map(TFrom key)
        {
            return !Mappings.ContainsKey(key) ? null : Mappings[key];
        }
    }
}
