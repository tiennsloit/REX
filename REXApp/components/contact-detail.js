import React, { Component } from 'react';
import { View, Text, Navigator } from 'react-native';

export default class ContactDetail extends Component {
  static get defaultProps() {
    return {
      title: 'Contact Detail',
      contact:'addd'
    };
  }

  render() {
    return (
      <View>
        <Text>{this.props.title}</Text>
      </View>
    )
  }
}