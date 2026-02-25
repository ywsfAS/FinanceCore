using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Category;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Categories
{
    public class Category : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public CategoryType Type { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsDefault { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Category() { }

        private Category(
            Guid categoryId,
            Guid userId,
            string name,
            CategoryType type,
            bool isActive,
            bool isDefault,
            string? description,
            DateTime createdAt,
            DateTime? updatedAt)
        {
            Id = categoryId;
            UserId = userId;
            Name = name;
            Type = type;
            IsActive = isActive;
            IsDefault = isDefault;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Reconstitute from persistence
        public static Category Create(
            Guid categoryId,
            Guid userId,
            string name,
            CategoryType type,
            bool isActive,
            bool isDefault,
            string? description,
            DateTime createdAt,
            DateTime? updatedAt = null)
        {
            return new Category(categoryId, userId, name, type, isActive, isDefault, description, createdAt, updatedAt);
        }

        // Create new category
        public static Category Create(
            Guid userId,
            string name,
            CategoryType type,
            string? description = null,
            bool isDefault = false)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidCategoryNameException(name, "Category name cannot be empty");

            if (name.Length > 50)
                throw new InvalidCategoryNameException(name, "Category name cannot exceed 50 characters");

            if (name.Length < 2)
                throw new InvalidCategoryNameException(name, "Category name must be at least 2 characters");

            if (!Enum.IsDefined(typeof(CategoryType), type))
                throw new InvalidCategoryTypeException(type);

            if (description != null && description.Length > 500)
                throw new InvalidCategoryDescriptionException(description, "Description cannot exceed 500 characters");

            var category = new Category
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name.Trim(),
                Type = type,
                Description = description?.Trim(),
                IsDefault = isDefault,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            category.AddDomainEvent(new CategoryCreatedEvent(
                category.Id,
                category.UserId,
                category.Name,
                category.Type));

            return category;
        }

        public void Update(string? name = null, string? description = null)
        {
            if (IsDefault)
                throw new DefaultCategoryModificationException(Id, Name, "update");

            if (!IsActive)
                throw new InactiveCategoryException(Id, Name, "update");

            var hasChanges = false;

            if (name != null && name != Name)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new InvalidCategoryNameException(name, "Category name cannot be empty");

                if (name.Length > 50)
                    throw new InvalidCategoryNameException(name, "Category name cannot exceed 50 characters");

                if (name.Length < 2)
                    throw new InvalidCategoryNameException(name, "Category name must be at least 2 characters");

                Name = name.Trim();
                hasChanges = true;
            }

            if (description != null && description != Description)
            {
                if (description.Length > 500)
                    throw new InvalidCategoryDescriptionException(description, "Description cannot exceed 500 characters");

                Description = description.Trim();
                hasChanges = true;
            }

            if (hasChanges)
            {
                UpdatedAt = DateTime.UtcNow;
                AddDomainEvent(new CategoryUpdatedEvent(Id, Name));
            }
        }

        public void ChangeType(CategoryType newType)
        {
            if (IsDefault)
                throw new DefaultCategoryModificationException(Id, Name, "change type");

            if (!IsActive)
                throw new InactiveCategoryException(Id, Name, "change type");

            if (!Enum.IsDefined(typeof(CategoryType), newType))
                throw new InvalidCategoryTypeException(newType);

            if (newType == Type)
                return;

            var oldType = Type;
            Type = newType;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new CategoryTypeChangedEvent(Id, oldType, newType));
        }

        public void Deactivate()
        {
            if (IsDefault)
                throw new DefaultCategoryModificationException(Id, Name, "deactivate");

            if (!IsActive)
                throw new CategoryAlreadyDeactivatedException(Id);

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new CategoryDeactivatedEvent(Id, Name));
        }

        public void Activate()
        {
            if (IsActive)
                throw new CategoryAlreadyActivatedException(Id);

            IsActive = true;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new CategoryActivatedEvent(Id, Name));
        }

        public bool CanBeUsedForTransactionType(EnTransactionType transactionType)
        {
            return Type switch
            {
                CategoryType.Income => transactionType == EnTransactionType.Income,
                CategoryType.Expense => transactionType == EnTransactionType.Expense,
                CategoryType.Both => true,
                _ => false
            };
        }
    }

}
