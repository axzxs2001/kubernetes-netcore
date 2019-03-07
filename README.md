# kubernetes-netcore
这是一个Ubuntu16.04为OS，kubernetes为部署平台，asp.net core为应用的测试项目。

### 安装kubernetes,kubeadm,docker
```
$ curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | apt-key add -  
$ cat <<EOF > /etc/apt/sources.list.d/kubernetes.list  
deb http://apt.kubernetes.io/ kubernetes-xenial main  
EOF  
$ apt-get update  
$ apt-get install -y docker.io kubeadm  
```
