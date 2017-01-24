var api = {
 getDrive(){
  
    var url = 'http://104.43.21.3/api/drive/1';
    return fetch(url).then((res) => res.json());
 }
};

module.exports = api;