import * as React from 'react';
import { render, screen } from '@testing-library/react';
import CircularProgressWithLabel from '../CircularProgressWithLabel'; // Adjust the import path as necessary
import "@testing-library/jest-dom";

describe('CircularProgressWithLabel Component', () => {
  it('renders the CircularProgress and shows the correct value', () => {
    // Render the component with a specific value (e.g., 50)
    render(<CircularProgressWithLabel value={50} />);

    // Verify the CircularProgress is rendered
    const progressElement = screen.getByRole('progressbar');
    expect(progressElement).toBeInTheDocument();

    // Verify the value text is correctly displayed
    const valueText = screen.getByText('50%');
    expect(valueText).toBeInTheDocument();
  });

  it('displays the correct percentage when the value prop changes', () => {
    // Render with an initial value of 25
    const { rerender } = render(<CircularProgressWithLabel value={25} />);

    // Check that the correct value is displayed
    let valueText = screen.getByText('25%');
    expect(valueText).toBeInTheDocument();

    // Rerender with a new value
    rerender(<CircularProgressWithLabel value={75} />);

    // Verify that the value is updated correctly
    valueText = screen.getByText('75%');
    expect(valueText).toBeInTheDocument();
  });

  it('displays 0% when the value is 0', () => {
    // Render with value 0
    render(<CircularProgressWithLabel value={0} />);

    // Verify the value text is 0%
    const valueText = screen.getByText('0%');
    expect(valueText).toBeInTheDocument();
  });

  it('displays 100% when the value is 100', () => {
    // Render with value 100
    render(<CircularProgressWithLabel value={100} />);

    // Verify the value text is 100%
    const valueText = screen.getByText('100%');
    expect(valueText).toBeInTheDocument();
  });
});
