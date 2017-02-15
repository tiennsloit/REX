import React, { Component } from 'react';
import Moment from 'moment';
import Icon from '../controls/icon';
import { Actions, ActionConst } from 'react-native-router-flux';
import Api from '../api/api';

import {
    StyleSheet,
    Text,
    View,
    Alert,
    Button,
    TouchableHighlight

} from 'react-native';


class ListOrdersItem extends Component {
    constructor(props) {
        super(props);
    }

    editOrder(id, contactId) {
        Actions.orderDetail({contactEditable:false,
            dataFunction: () => { return Api.getOrder(id); }, routeSaveFunction: () => {
               Actions.pop();
            }
        }); 

    }

    render() {
        if (this.props.rowData.isNew) {
            return (
                <TouchableHighlight >
                    <Text onPress={() => this.editOrder(this.props.rowData.id, this.props.rowData.contactId)}>ship at: {Moment(this.props.rowData.dateShipped).format('DD-MM-YYYY')}  <Icon name="edit" size={15} /></Text>
                </TouchableHighlight>
            );
        }
        else {
            return (
                <TouchableHighlight >
                    <Text onPress={() => this.editOrder(this.props.rowData.id, this.props.rowData.contactId)}>{Moment(this.props.rowData.dateShipped).format('DD-MM-YYYY')} <Icon name="ok" size={20} /></Text>
                </TouchableHighlight>
            );
        }

    }
}

const styles = StyleSheet.create({

    listViewItem: {
        height: 40,
        marginLeft: 12,
        fontSize: 16,
        paddingTop: 10
    },
    newOrder: {
        backgroundColor: 'blue',
        color: 'white'
    }
});

export default ListOrdersItem;