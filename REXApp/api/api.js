var api = {
 getContacts(){
  
    var url = 'http://rexwebapi.azurewebsites.net/api/contact';
    return fetch(url).then((res) => res.json());
 },

 getOrderDefaultNewContact()
 {
     var url = 'http://rexwebapi.azurewebsites.net/getorderByDefault/2';
     return fetch(url).then((res)=> res.json());
 },

 getAllListItems()
 {
     var url = 'http://rexwebapi.azurewebsites.net/api/list';
     return fetch(url).then((res)=> res.json());
 }

};

module.exports = api;