import React, { Component } from 'react';
import { View, Text, Navigator } from 'react-native';
import ListContact from '../components/list-contacts';

export default class SceneMain extends Component {
  static get defaultProps() {
    return {
      title: 'Contact List'
    };
  }

  render() {
    return (
      <ListContact/>
    )
  }
}