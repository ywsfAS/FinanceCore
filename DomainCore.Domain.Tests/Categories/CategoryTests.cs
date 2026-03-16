using System;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using Xunit;
using FluentAssertions;

namespace FinanceCore.Domain.Tests.Categories
{
    public class CategoryTests
    {
        [Fact]
        public void CreateCategory_ShouldInitializeProperly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var name = "Food";
            var type = CategoryType.Expense;
            var description = "Monthly food expenses";

            // Act
            var category = Category.Create(userId, name, type, description);

            // Assert
            category.UserId.Should().Be(userId);
            category.Name.Should().Be(name);
            category.Type.Should().Be(type);
            category.Description.Should().Be(description);
            category.IsActive.Should().BeTrue();
            category.IsDefault.Should().BeFalse();
            category.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void CreateCategory_WithInvalidName_ShouldThrow()
        {
            var userId = Guid.NewGuid();

            Action actEmpty = () => Category.Create(userId, "", CategoryType.Expense);
            Action actShort = () => Category.Create(userId, "A", CategoryType.Expense);
            Action actLong = () => Category.Create(userId, new string('x', 51), CategoryType.Expense);

            actEmpty.Should().Throw<InvalidCategoryNameException>();
            actShort.Should().Throw<InvalidCategoryNameException>();
            actLong.Should().Throw<InvalidCategoryNameException>();
        }

        [Fact]
        public void CreateCategory_WithInvalidUserId_ShouldThrow()
        {
            Action act = () => Category.Create(Guid.Empty, "Food", CategoryType.Expense);
            act.Should().Throw<ArgumentException>().WithMessage("User ID cannot be empty.*");
        }

        [Fact]
        public void UpdateCategory_ShouldChangeNameAndDescription()
        {
            var category = Category.Create(Guid.NewGuid(), "Food", CategoryType.Expense, "Old desc");

            category.Update("Groceries", "New desc");

            category.Name.Should().Be("Groceries");
            category.Description.Should().Be("New desc");
            category.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void UpdateCategory_DefaultOrInactive_ShouldThrow()
        {
            var category = Category.Create(Guid.NewGuid(), "Food", CategoryType.Expense);

            // Mark default
            var defaultCategory = Category.Create(Guid.NewGuid(), "Default", CategoryType.Expense, isDefault: true);

            Action actDefault = () => defaultCategory.Update("NewName");
            actDefault.Should().Throw<DefaultCategoryModificationException>();

            category.Deactivate();
            Action actInactive = () => category.Update("NewName");
            actInactive.Should().Throw<InactiveCategoryException>();
        }

        [Fact]
        public void ChangeType_ShouldUpdateCategoryType()
        {
            var category = Category.Create(Guid.NewGuid(), "Food", CategoryType.Expense);

            category.ChangeType(CategoryType.Both);

            category.Type.Should().Be(CategoryType.Both);
            category.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void ChangeType_InvalidOrDefaultOrInactive_ShouldThrow()
        {
            var category = Category.Create(Guid.NewGuid(), "Food", CategoryType.Expense);
            var defaultCategory = Category.Create(Guid.NewGuid(), "Default", CategoryType.Expense, isDefault: true);

            Action actDefault = () => defaultCategory.ChangeType(CategoryType.Income);
            actDefault.Should().Throw<DefaultCategoryModificationException>();

            category.Deactivate();
            Action actInactive = () => category.ChangeType(CategoryType.Income);
            actInactive.Should().Throw<InactiveCategoryException>();

            Action actInvalid = () => Category.Create(Guid.NewGuid(), "Test", CategoryType.Expense).ChangeType(CategoryType.Income);
            actInvalid.Should().Throw<InvalidCategoryTypeException>();
        }

        [Fact]
        public void DeactivateAndActivateCategory_ShouldWorkCorrectly()
        {
            var category = Category.Create(Guid.NewGuid(), "Food", CategoryType.Expense);

            category.Deactivate();
            category.IsActive.Should().BeFalse();

            Action actDeactivateAgain = () => category.Deactivate();
            actDeactivateAgain.Should().Throw<CategoryAlreadyDeactivatedException>();

            category.Activate();
            category.IsActive.Should().BeTrue();

            Action actActivateAgain = () => category.Activate();
            actActivateAgain.Should().Throw<CategoryAlreadyActivatedException>();
        }

        [Fact]
        public void CanBeUsedForTransactionType_ShouldReturnCorrectly()
        {
            var expenseCategory = Category.Create(Guid.NewGuid(), "Food", CategoryType.Expense);
            var incomeCategory = Category.Create(Guid.NewGuid(), "Salary", CategoryType.Income);
            var bothCategory = Category.Create(Guid.NewGuid(), "Misc", CategoryType.Both);

            expenseCategory.CanBeUsedForTransactionType(EnTransactionType.Expense).Should().BeTrue();
            expenseCategory.CanBeUsedForTransactionType(EnTransactionType.Income).Should().BeFalse();

            incomeCategory.CanBeUsedForTransactionType(EnTransactionType.Income).Should().BeTrue();
            incomeCategory.CanBeUsedForTransactionType(EnTransactionType.Expense).Should().BeFalse();

            bothCategory.CanBeUsedForTransactionType(EnTransactionType.Expense).Should().BeTrue();
            bothCategory.CanBeUsedForTransactionType(EnTransactionType.Income).Should().BeTrue();
        }
    }
}
