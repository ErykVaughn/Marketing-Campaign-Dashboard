module.exports = {
  transform: {
    "^.+\\.[t|j]sx?$": "babel-jest"
  },
  moduleNameMapper: {
    "\\.css$": "identity-obj-proxy"
  },
  testEnvironment: 'jsdom', // Ensure the correct test environment
  testMatch: ['**/__tests__/**/*.js?(x)'], // Ensure it looks for tests in the right location
};
