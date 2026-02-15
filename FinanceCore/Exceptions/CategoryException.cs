using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Exceptions
{
    // When category doesn't exist
    public class CategoryNotFoundException : DomainException
    {
        public Guid CategoryId { get; }

        public CategoryNotFoundException(Guid categoryId)
            : base($"Category with ID '{categoryId}' was not found.")
        {
            CategoryId = categoryId;
        }
    }

    // When trying to operate on inactive category
    public class InactiveCategoryException : DomainException
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }

        public InactiveCategoryException(Guid categoryId, string categoryName)
            : base($"Category '{categoryName}' (ID: {categoryId}) is inactive.")
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        public InactiveCategoryException(Guid categoryId, string categoryName, string operation)
            : base($"Cannot {operation} with inactive category '{categoryName}' (ID: {categoryId}).")
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }

    // When category name is invalid
    public class InvalidCategoryNameException : DomainException
    {
        public string ProvidedName { get; }

        public InvalidCategoryNameException(string providedName, string reason)
            : base($"Invalid category name '{providedName}': {reason}")
        {
            ProvidedName = providedName;
        }
    }

    // When category type is invalid
    public class InvalidCategoryTypeException : DomainException
    {
        public CategoryType Type { get; }

        public InvalidCategoryTypeException(CategoryType type)
            : base($"Invalid category type: {type}")
        {
            Type = type;
        }

        public InvalidCategoryTypeException(string reason)
            : base($"Invalid category type: {reason}")
        {
        }
    }

    // When trying to modify default/system category
    public class DefaultCategoryModificationException : DomainException
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }
        public string Operation { get; }

        public DefaultCategoryModificationException(Guid categoryId, string categoryName, string operation)
            : base($"Cannot {operation} default/system category '{categoryName}' (ID: {categoryId})")
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            Operation = operation;
        }
    }

    // When duplicate category exists
    public class DuplicateCategoryException : DomainException
    {
        public Guid UserId { get; }
        public string CategoryName { get; }

        public DuplicateCategoryException(Guid userId, string categoryName)
            : base($"A category with name '{categoryName}' already exists for user {userId}")
        {
            UserId = userId;
            CategoryName = categoryName;
        }
    }

    // When category has active transactions
    public class CategoryHasTransactionsException : DomainException
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }
        public int TransactionCount { get; }

        public CategoryHasTransactionsException(Guid categoryId, string categoryName, int transactionCount)
            : base($"Cannot delete category '{categoryName}' because it has {transactionCount} transaction(s)")
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            TransactionCount = transactionCount;
        }
    }

    // When category has active budgets
    public class CategoryHasBudgetsException : DomainException
    {
        public Guid CategoryId { get; }
        public string CategoryName { get; }
        public int BudgetCount { get; }

        public CategoryHasBudgetsException(Guid categoryId, string categoryName, int budgetCount)
            : base($"Cannot delete category '{categoryName}' because it has {budgetCount} active budget(s)")
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            BudgetCount = budgetCount;
        }
    }

    // When category has dependencies
    public class CategoryHasDependenciesException : DomainException
    {
        public Guid CategoryId { get; }
        public string Dependencies { get; }

        public CategoryHasDependenciesException(Guid categoryId, string dependencies)
            : base($"Cannot delete category {categoryId}. It has dependencies: {dependencies}")
        {
            CategoryId = categoryId;
            Dependencies = dependencies;
        }
    }

    // When category already activated
    public class CategoryAlreadyActivatedException : DomainException
    {
        public Guid CategoryId { get; }

        public CategoryAlreadyActivatedException(Guid categoryId)
            : base($"Category {categoryId} is already activated.")
        {
            CategoryId = categoryId;
        }
    }

    // When category already deactivated
    public class CategoryAlreadyDeactivatedException : DomainException
    {
        public Guid CategoryId { get; }

        public CategoryAlreadyDeactivatedException(Guid categoryId)
            : base($"Category {categoryId} is already deactivated.")
        {
            CategoryId = categoryId;
        }
    }

    // When parent category doesn't exist (for subcategories)
    public class ParentCategoryNotFoundException : DomainException
    {
        public Guid ParentCategoryId { get; }

        public ParentCategoryNotFoundException(Guid parentCategoryId)
            : base($"Parent category with ID '{parentCategoryId}' was not found.")
        {
            ParentCategoryId = parentCategoryId;
        }
    }

    // When trying to create circular reference (category as its own parent)
    public class CircularCategoryReferenceException : DomainException
    {
        public Guid CategoryId { get; }
        public Guid ParentCategoryId { get; }

        public CircularCategoryReferenceException(Guid categoryId, Guid parentCategoryId)
            : base($"Cannot set category {categoryId} as child of {parentCategoryId}: would create circular reference")
        {
            CategoryId = categoryId;
            ParentCategoryId = parentCategoryId;
        }
    }

    // When category type doesn't match parent type
    public class CategoryTypeMismatchException : DomainException
    {
        public CategoryType ChildType { get; }
        public CategoryType ParentType { get; }

        public CategoryTypeMismatchException(CategoryType childType, CategoryType parentType)
            : base($"Child category type {childType} must match parent category type {parentType}")
        {
            ChildType = childType;
            ParentType = parentType;
        }
    }

    // When trying to use category for wrong transaction type
    public class CategoryTransactionTypeMismatchException : DomainException
    {
        public Guid CategoryId { get; }
        public CategoryType CategoryType { get; }
        public EnTransactionType TransactionType { get; }

        public CategoryTransactionTypeMismatchException(
            Guid categoryId,
            CategoryType categoryType,
            EnTransactionType transactionType)
            : base($"Cannot use {categoryType} category for {transactionType} transaction")
        {
            CategoryId = categoryId;
            CategoryType = categoryType;
            TransactionType = transactionType;
        }
    }

    // When category description is too long
    public class InvalidCategoryDescriptionException : DomainException
    {
        public string Description { get; }

        public InvalidCategoryDescriptionException(string description, string reason)
            : base($"Invalid category description: {reason}")
        {
            Description = description;
        }
    }

    // When maximum category limit reached
    public class CategoryLimitExceededException : DomainException
    {
        public Guid UserId { get; }
        public int MaxCategories { get; }

        public CategoryLimitExceededException(Guid userId, int maxCategories)
            : base($"User {userId} has reached the maximum limit of {maxCategories} categories")
        {
            UserId = userId;
            MaxCategories = maxCategories;
        }
    }
}
