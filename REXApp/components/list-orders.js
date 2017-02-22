import React, { Component } from 'react';
import Header from '../components/list-view-header';
import Api from '../api/api';
import Loading from '../components/loading';
import ListOrdersItem from '../components/list-orders-item';
import { SwipeListView } from 'react-native-swipe-list-view';
import Icon from '../controls/icon';
import { Actions } from 'react-native-router-flux';
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
class ListOrders extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [],
            hasData: false,
            mainListData: ds.cloneWithRows([])

        }
        debugger;
        this.fetchData();
    }

    fetchData() {
        debugger;
        //navigate from the list contact -> new order
        if (!this.props || !this.props.id) {
            Actions.orderDetail({
                contactEditable: true,
                dataFunction: () => { return Api.getOrderDefaultNewContact(); }, routeSaveFunction: () => {
                    Actions.pop();
                }
            });
        }//end
        else {
            Api.getOrders(this.props.id).then(res => {
                this.setState({
                    items: res,
                    hasData: true,
                    mainListData: ds.cloneWithRows(res)
                });
            });
        }


    }

    deleteOrder(order, secId, rowId, rowMap) {
        this.removeItemList(secId, rowId, rowMap);
        Api.deleteOrder(order.id).then(() => {
            this.fetchData();
        });
    }

    finishOrder(order, secId, rowId, rowMap) {
        this.removeItemList(secId, rowId, rowMap);
        Api.finishOrder(order.id).then(() => {
            this.fetchData();
        });
    }

    removeItemList(secId, rowId, rowMap) {
        rowMap[`${secId}${rowId}`].closeRow();
    }

    newOrder(contactId) {

        Actions.orderDetail({
            contactEditable: false,
            dataFunction: () => { return Api.getOrderDefaultExistingContact(contactId); }, routeSaveFunction: () => {
                Actions.tabOrs({ type: "reset", id: contactId });

            }
        });
    }

    render() {
        if (!this.props || !this.props.id) {
            return (<View style={styles.containerEmpty}><Text style={styles.messageEmpty}>There is no order for this contact! </Text></View>);
        }
        return (
            <View style={styles.container}>
                <SwipeListView style={styles.contentContainer}
                    dataSource={this.state.mainListData}
                    renderRow={(rowData) =>
                        <View style={styles.rowFront}><ListOrdersItem rowData={rowData} /></View>
                    }
                    enableEmptySections={true}
                    renderSeparator={(sectionId, rowId) => <View key={rowId} style={styles.separator} />}
                    contentContainerStyle={styles.listView}
                    renderHeader={() => this.state.hasData == true ? <Header /> : <Text></Text>}
                    renderHiddenRow={(data, secId, rowId, rowMap) => (
                        <View style={styles.rowBack}>
                            <Text onPress={(order) => this.finishOrder(data, secId, rowId, rowMap)}><Icon name="ok" color="white" size={20} /></Text>
                            <Text onPress={(order) => this.deleteOrder(data, secId, rowId, rowMap)}><Icon name="trash-empty" color="white" size={20} /></Text>
                        </View>
                    )}
                    leftOpenValue={50}
                    rightOpenValue={-50}
                />
                <Icon.Button style={styles.newOrderButton} name="floppy" onPress={() => this.newOrder(this.props.id)} >NEW ORDER</Icon.Button>

            </View>
        );
    }
}

const styles = StyleSheet.create({

    listView: {
        justifyContent: 'center',
    },
    container: {
        flex: 1,
        marginBottom: 50

    },

    containerEmpty: {
        marginTop: 60,
    },

    messageEmpty: {
        textAlign: 'center'
    },

    newOrderButton: {
        backgroundColor: "darkorange",
        borderRadius: 0,
        justifyContent: 'center'
    },

    contentContainer: {
        flex: 1, // pushes the footer to the end of the screen
        marginTop: 60,
        padding: 10
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

    rowFront: {
        alignItems: 'center',
        backgroundColor: 'white',
        borderBottomColor: 'black',
        borderBottomWidth: 1,
        justifyContent: 'center',
        height: 50,
    },
    rowBack: {
        alignItems: 'center',
        backgroundColor: 'red',
        flex: 1,
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingLeft: 12,
        paddingRight: 12
    }
});

export default ListOrders;