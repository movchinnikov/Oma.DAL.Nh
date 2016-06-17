namespace Oma.DAL.Nh
{
    public struct ListPage
    {
        public ListPage(int rowCount, int offSet, int totalCount)
        {
            _rowCount = rowCount;
            _offSet = offSet;
            _totalCount = totalCount;
        }
        private readonly int _rowCount;
        public int RowCount
        {
            get { return _rowCount; }
        }

        private readonly int _offSet;
        public int Offset
        {
            get { return _offSet; }
        }

        private readonly int _totalCount;
        public int TotalCount
        {
            get { return _totalCount; }
        }
    }
}