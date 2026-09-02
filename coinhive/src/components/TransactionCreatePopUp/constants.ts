export const CREATE_TRANSACTION_COPY = {
    title: "Create a New Transaction",
    description: "Record your financial activity to keep your balances and reports up to date.",
    fields: {
        account: { label: "Account", description: "Select the account connected to this transaction." },
        category: { label: "Category", description: "Choose a category to organize this activity." },
        type: { label: "Transaction Type", description: "Select how this transaction affects your finances." },
        amount: { label: "Amount", description: "Enter the amount for this transaction." },
        description: { label: "Description", description: "Add a note to help you recognize this transaction later." },
    },
    submit: "Create Transaction",
};