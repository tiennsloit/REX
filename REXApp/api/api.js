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

    deleteOrder()
    {
        var url = 'http://rexwebapi.azurewebsites.net/api/list';
        return fetch(url).then((res) => res.json());
    }

    saveOrder(order) {
        
        var url = 'http://rexwebapi.azurewebsites.net/api/order/postorder';
        fetch(url, {
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