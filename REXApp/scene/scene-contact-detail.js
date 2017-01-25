import React, { Component } from 'react';
import { View, Text, Navigator } from 'react-native';

export default class SceneContactDetail extends Component {
  static get defaultProps() {
    return {
      title: 'Contact Detail'
    };
  }

  render() {
    return (
      <View>
        <Text>Detail</Text>
      </View>
    )
  }
}