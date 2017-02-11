var api = {
    getContacts() {

        var url = 'http://rexwebapi.azurewebsites.net/api/contact';
        return fetch(url).then((res) => res.json());
    },

    getOrders(contactId){
        debugger;
        var url = 'http://rexwebapi.azurewebsites.net/getOrders/' + contactId;
        return fetch(url).then((res) => res.json());
    },

    getOrderDefaultNewContact() {
        var url = 'http://rexwebapi.azurewebsites.net/getorderByDefault/2';
        return fetch(url).then((res) => res.json());
    },

    getAllListItems() {
        var url = 'http://rexwebapi.azurewebsites.net/api/list';
        return fetch(url).then((res) => res.json());
    },

    deleteOrder(id)
    {
        var url = 'http://rexwebapi.azurewebsites.net/api/order/' + id;
        return fetch(url, {
            method: 'DELETE'
        }).then((res) => res.json());
    },
    finishOrder(id)
    {
        var url = 'http://rexwebapi.azurewebsites.net/finishOrder/' + id;
        return fetch(url, {
            method: 'PUT'
        }).then((res) => {
        });
    },

    saveOrder(order) {
        
        var url = 'http://rexwebapi.azurewebsites.net/api/order/postorder';
        return fetch(url, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(order)
        });
    }

};

module.exports = api;