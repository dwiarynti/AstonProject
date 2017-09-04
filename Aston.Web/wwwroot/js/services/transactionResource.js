(function () {
    "use strict";
    angular
        .module("common.services")
        .factory("transactionResource",
                ["$resource",
                 transactionResource]);
    function transactionResource($resource) {
        return $resource(virtualDirectory + "/api/Transaction/:action/:BranchID/:TransactionID/:UpdatedBy",
        { id: '@id' },
        {
            reconcile: {
                method: 'POST',
                params: { action: 'mainVaultReconcile' },
            },
            add: { method: 'POST', params: { action: 'add' } },
            register: {
                method: 'POST',
                params: { action: 'register' },
            },
            search: { method: 'POST', params: { action: 'searchTransactionList' } },
            registerWithdrawal: { method: 'POST', params: { action: 'registerWithDrawalRegister' } },
            registerDeposit: { method: 'POST', params: { action: 'registerDepositRegister' } },
            registerExchange: { method: 'POST', params: { action: 'registerExchangeRegister' } },

            update: {
                method: 'POST',
                params: { action: 'UpdateMainvaultTransaction' },
            },
            deleteMainvaultTransaction: { method: 'POST', params: { action: 'deleteMainVaultCashInOutDetails' } },
            updateCashBalanceTransaction: {
                method: 'POST',
                params: { action: 'updateOpenBalanceDetails' },
            },
            deleteOpenBalanceDetails: { method: 'POST', params: { action: 'deleteOpenBalanceDetails' } },
            updateDepositDetails: {
                method: 'POST',
                params: { action: 'updateDepositDetails' },
            },
            deleteDepositDetails: { method: 'POST', params: { action: 'deleteDepositDetails' } },
            insertMainVaultMisMatch: { method: 'POST', params: { action: 'insertMainVaultMisMatch' } },

        });
    }
}());