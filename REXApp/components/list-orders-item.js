import React, { Component } from 'react';
import Moment from 'moment';
import Icon from '../controls/icon';

import {
    StyleSheet,
    Text,
    View,
    Alert,
    Button,
    TouchableHighlight

} from 'react-native';


class ListOrdersItem extends Component {
    render() {
        if (this.props.rowData.isNew) {
            return (
                <TouchableHighlight >
                    <Text>{Moment(this.props.rowData.dateShipped).format('DD-MM-YYYY')}  <Icon name="edit" size={15}/></Text>
                </TouchableHighlight>
            );
        }
        else {
            return (
                <TouchableHighlight >
                    <Text>{Moment(this.props.rowData.dateShipped).format('DD-MM-YYYY')} <Icon name="ok" size={20}/></Text>
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
    newOrder:{
        backgroundColor:'blue',
        color:'white'
    }
});

export default ListOrdersItem;