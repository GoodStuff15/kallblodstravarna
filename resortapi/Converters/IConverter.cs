namespace resortapi.Converters
{
    public interface IConverter<T, V>
    {
        public T FromDTOtoObject(V dto);

        public V FromObjecttoDTO(T obj);

    }
}
