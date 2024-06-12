namespace targheX.Services
{
    public interface IYearService
    {
        bool CloseYearInternal(int year);
        void CreateNewYearTable(int newYear);
        bool IsYearClosed(int year);
    }
}