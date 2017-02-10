import React, { Component } from 'react';
import Header from '../components/list-view-header';
import Api from '../api/api';
import Loading from '../components/loading';
import Moment from 'moment';
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
        debugger;
        Api.getOrders(this.props.id).then(res => {
            this.setState({
                items: res,
                hasData: true,
                mainListData: ds.cloneWithRows(res)
            });
        });
    }

    render() {
        return (
            <View style={styles.container}>
                <ListView style={styles.contentContainer}
                    dataSource={this.state.mainListData}
                    renderRow={(rowData) =>
                        <TouchableHighlight >
                            <Text style={styles.listViewItem}>{Moment(rowData.dateShipped).format('DD-MM-YYYY')}</Text>
                        </TouchableHighlight>
                    }
                    renderSeparator={(sectionId, rowId) => <View key={rowId} style={styles.separator} />}
                    contentContainerStyle={styles.listView}
                    renderHeader={() => this.state.hasData == true ? <Header /> : <Text></Text>}
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
    newOrderButton: {
        height:100
    }
});

export default ListOrders;