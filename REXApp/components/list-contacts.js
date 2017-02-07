import React, { Component } from 'react';
import ReactNative from 'react-native';
import Api from '../api/api';
import Header from '../components/list-view-header';
import {Actions} from 'react-native-router-flux';
import {
    StyleSheet,
    Text,
    View,
    Alert,
    Button,
    ListView,
    TouchableHighlight
    
} from 'react-native';

const ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 });
class ListContact extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [],
            hasData: false,
            mainListData: ds.cloneWithRows([]),
            orderNew:null
        }

        this.fetchData();
    }

    fetchData() {

        Api.getContacts().then(res => {
            this.setState({
                items: res,
                hasData: true,
                mainListData: ds.cloneWithRows(res)
            });
        });
    }

    filterData(text) {
        var filteredItems = [];
        if (text != '') {
            filteredItems = this.state.items.filter((item) => {
                return item.name.toString().toLowerCase().indexOf(text.toString().toLowerCase()) >= 0 ? true : false;
            });
        }

        this.setState({
            mainListData: ds.cloneWithRows(filteredItems)
        });
    }

    createNewOrder(){
        Actions.orderDetail({id:0});
    }

    showDetail(contact){
       Actions.contactDetail(contact);
    }

    render() {
        if(this.state.items.length == 0)
        {
            return (
                <View>
                       <Text>Loading...</Text>
                       <Button onPress={() => this.createNewOrder()} title="New order" />
                </View>
            )
        }
        return (
            <View>
                <ListView style={styles.container}
                    dataSource={this.state.mainListData}
                    renderRow={(rowData) => 
                        <TouchableHighlight onPress={()=>{this.showDetail(rowData)}}>
                        <Text  style={styles.listViewItem}>{rowData.name}</Text>
                        </TouchableHighlight>
                    }
                    renderSeparator={(sectionId, rowId) => <View key={rowId} style={styles.separator} />}
                    contentContainerStyle={styles.listView}
                    renderHeader={() => this.state.hasData == true ? <Header filterDataFunction={(text) => this.filterData(text)} /> : <Text></Text>}
                    />
                <Button onPress={() => this.createNewOrder()} title="New order" />
            </View>
        );
    }
}

const styles = StyleSheet.create({

    listView: {
        justifyContent: 'center',
    },
    container: {
        padding: 12,
    },
    welcome: {
        fontSize: 20,
        textAlign: 'center',
        margin: 10,
    },

    text: {
        color: 'black',
    },
    buttonCall: {
        width: 10,
        textAlign: 'right'
    },
    listViewItem: {
        height: 40,
        marginLeft: 12,
        fontSize: 16,
        paddingTop: 10
    },
    separator: {
        flex: 1,
        height: StyleSheet.hairlineWidth,
        backgroundColor: '#8E8E8E',
    },
});

export default ListContact;