# Use an official Nginx image as the base image
FROM nginx:alpine

# Copy static files to the default Nginx public folder
COPY . /usr/share/nginx/html

# Expose port 80 to access the app
EXPOSE 80

# No need for a CMD, the base Nginx image handles that
