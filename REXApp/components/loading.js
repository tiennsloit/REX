import React, { Component } from 'react';
import {Text, StyleSheet} from 'react-native';
import Icon from '../controls/icon';
class Loading extends Component {
    render() {
        return (
            <Text style={styles.loading}><Icon name="spin1" size={20}/></Text>
        );
    }
}

const styles = StyleSheet.create({

    loading: {
        textAlign: 'center',
        paddingTop: 10,
        paddingBottom:10
    }
});

export default Loading;