import React, { Component } from 'react';
import Header from '../components/list-view-header';
import Api from '../api/api';
import Loading from '../components/loading';
import ListOrdersItem from '../components/list-orders-item';
import { SwipeListView } from 'react-native-swipe-list-view';
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

        this.fetchData();
    }

    fetchData() {
        Api.getOrders(this.props.id).then(res => {
            this.setState({
                items: res,
                hasData: true,
                mainListData: ds.cloneWithRows(res)
            });
        });
    }

    deleteOrder(order){
        debugger;
    }

    render() {
        return (
            <View style={styles.container}>
                <SwipeListView style={styles.contentContainer}
                    dataSource={this.state.mainListData}
                    renderRow={(rowData) =>
                        <View style={styles.rowFront}><ListOrdersItem rowData={rowData}/></View>
                    }
                    renderSeparator={(sectionId, rowId) => <View key={rowId} style={styles.separator} />}
                    contentContainerStyle={styles.listView}
                    renderHeader={() => this.state.hasData == true ? <Header /> : <Text></Text>}
                    renderHiddenRow={ data => (
                <View style={styles.rowBack}>
                    <Text onPress={(order)=>this.deleteOrder(data)}>Delete</Text>
                    <Text onPress={(order)=>this.deleteOrder(data)}>Delete</Text>
                </View>
            )}
            leftOpenValue={75}
            rightOpenValue={-75}
                />
                <Button style={styles.newOrderButton} color="grey" title="New order" />
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
       
    },
    
    contentContainer: {
        flex: 1, // pushes the footer to the end of the screen
        padding: 0,
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
        color:'white',
		flex: 1,
		flexDirection: 'row',
		justifyContent: 'space-between',
		paddingLeft: 12,
	}
});

export default ListOrders;