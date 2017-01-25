/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */
import React, { Component } from 'react';
import ReactNative from 'react-native';
import DatePicker from './controls/date-picker.android.js';
import ListContact from './components/list-contacts';

import {
  AppRegistry,
  StyleSheet,
  View,
  Picker
} from 'react-native';

export default class REXApp extends Component {
  render() {
    return (
      <View>
        <ListContact />
      </View>
    );
  }
}

const styles = StyleSheet.create({
  picker: {
    width: 100,
  }
});

AppRegistry.registerComponent('REXApp', () => REXApp);
