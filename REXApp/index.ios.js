/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */
import React, { Component } from 'react';
import ReactNative from 'react-native';
import DatePicker from './controls/date-picker.android.js';
import NavigatorMain from './components/navigator-main';

import {
  AppRegistry,
  StyleSheet,
  View,
  Text,
  Picker,
  Navigator
} from 'react-native';

export default class REXApp extends Component {

  render() {
    
    return (
      <NavigatorMain/>
    );
  }
}

const styles = StyleSheet.create({
  picker: {
    width: 100,
  }
});

AppRegistry.registerComponent('REXApp', () => REXApp);
