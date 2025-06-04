namespace resortapi.Converters
{
    public interface IConverter<T, V>
    {
        public T FromDTOtoObject(V dto);

        public V FromObjecttoDTO(T obj);

        public ICollection<V> FromObjecttoDTO_Collection(ICollection<T> collection);
        public ICollection<T> FromDTOtoObject_Collection(ICollection<V> collection);
        public ICollection<V> FromObjectCollection_ToOverviewCollection(ICollection<T> objects);
    }
}
