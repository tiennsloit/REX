import React, {
  PropTypes,
} from 'react';
import {
  Text,StyleSheet
} from 'react-native';

const propTypes = {
  selected: PropTypes.bool,
  title: PropTypes.string,
};

const TabIcon = (props) => (
  <Text
    style={{ color: props.selected ? 'black' : 'white' }}
  >
    {props.title}
  </Text>
);

TabIcon.propTypes = propTypes;

const styles = StyleSheet.create({
  tab:{
    
  }
});

export default TabIcon;