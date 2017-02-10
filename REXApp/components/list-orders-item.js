import React, { Component } from 'react';
import Moment from 'moment';

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
        return (
           
                <TouchableHighlight >
                            <Text>{Moment(this.props.rowData.dateShipped).format('DD-MM-YYYY')}</Text>
                </TouchableHighlight>
           
            
        );
    }
}

const styles = StyleSheet.create({

    listViewItem: {
        height: 40,
        marginLeft: 12,
        fontSize: 16,
        paddingTop: 10
    }
});

export default ListOrdersItem;