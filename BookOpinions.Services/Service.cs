using BookOpinions.Data;

namespace BookOpinions.Services
{
    public abstract class Service
    {
        private BookOpinionsDbContext context;
        protected BookOpinionsDbContext Context => this.context;

        public Service()
            : this(new BookOpinionsDbContext())
        {
        }

        public Service(BookOpinionsDbContext context)
        {
            this.context = context;
        }
    }
}
