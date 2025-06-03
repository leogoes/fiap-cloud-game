using FIAP.Cloud.Games.Core.Exceptions;

namespace FIAP.Cloud.Games.Core.Domains
{
    public abstract class Entity
    {
        int? _requestedHashCode;
        Guid _Id;

        public bool? Error { get; set; } = false;
        private List<ErrorDetail> _Errors { get; set; } = [];
        public IReadOnlyCollection<ErrorDetail> Errors => _Errors;

        public virtual Guid Id { get; protected set; }

        protected Entity()
        {
            _Id = Guid.NewGuid();
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not Entity)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (GetType() != obj.GetType())
                return false;
            Entity item = (Entity)obj;
            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
                return Equals(right, null);
            else
                return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public void AddError(ErrorDetail error)
        {
            _Errors ??= [];

            _Errors.Add(error);
            Error = true;
        }
    }
}
