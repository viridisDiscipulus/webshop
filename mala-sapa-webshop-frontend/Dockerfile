# Use an official Nginx image
FROM nginx:alpine

# Copy the built Angular files to the Nginx default HTML directory
COPY ./dist/mala-sapa-webshop-frontend /usr/share/nginx/html

# Expose port 80 for the container
EXPOSE 80

# Start Nginx when the container starts
CMD ["nginx", "-g", "daemon off;"]
