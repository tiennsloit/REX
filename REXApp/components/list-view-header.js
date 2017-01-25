import React, { Component } from 'react';
import { View, Text, StyleSheet, TextInput } from 'react-native';

class Header extends Component {
    filterData(text) {
      
        this.props.filterDataFunction(text);
    }
    render() {
        return (
           <View style={styles.container}>
                    <TextInput
                        style={styles.input}
                        placeholder="Search..."
                        onChangeText={(text) => this.filterData(text)}
                        />
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 2,
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#C1C1C1',
    },
    input: {
        height: 40,
        flex: 1,
        paddingHorizontal: 8,
        fontSize: 15,
        backgroundColor: '#FFFFFF',
        borderRadius: 0,
    },
});

export default Header;