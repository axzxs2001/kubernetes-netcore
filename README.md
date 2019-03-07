# kubernetes-netcore
这是一个Ubuntu16.04为OS，kubernetes为部署平台，asp.net core为应用的测试项目。

### 安装kubernetes,kubeadm,docker
切换到到root下（su -），执行下面命令
```
$ curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | apt-key add -  
$ cat <<EOF > /etc/apt/sources.list.d/kubernetes.list  
deb http://apt.kubernetes.io/ kubernetes-xenial main  
EOF  
$ apt-get update  
$ apt-get install -y docker.io kubeadm  
```
### 修改配置文件并复制配置文件
修改/etc/systemd/system/kubelet.service.d/10-kubeadm.conf  
```
Environment="KUBELET_KUBECONFIG_ARGS=--bootstrap-kubeconfig=/etc/kubernetes/bootstrap-kubelet.conf --kubeconfig=/etc/kubernetes/kubelet.conf --fail-swap-on=false"
```
```
mkdir -p $HOME/.kube  
sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config  
sudo chown $(id -u):$(id -g) $HOME/.kube/config  
```
### 执行master初始化
```
sudo kubeadm init --config kubeadm.yaml --ignore-preflight-errors=swap
```
会生成如下的命令，这个命令用来加入从节点
```
kubeadm join 192.168.252.54:6443 --token jetzdj.7ycrb79mihrlrggq --discovery-token-ca-cert-hash sha256:f8a25957a41d187587a46a0af43c9b715e7e2d903473a9d4e0cad5009a5031ba
```
### 安装网络插件
```
sudo kubectl apply -f https://git.io/weave-kube-1.6
```
查看结果：
```
sudo kubectl get pods -n kube-system
```
### 安装配置dashboard
#### 安装
```
sudo kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/master/aio/deploy/recommended/kubernetes-dashboard.yaml
```
#### 配置用户和角色
```
sudo kubectl apply -f adminuser.yaml
sudo kubectl apply -f adminrole.yaml
```
#### 获取dashboard登录token
```
sudo kubectl -n kube-system describe secret $(kubectl -n kube-system get secret | grep admin-user | awk '{print $1}')
```
#### 登录
http://localhost:8001/api/v1/namespaces/kube-system/services/https:kubernetes-dashboard:/proxy/

### 单节点部署
```
sudo kubectl taint nodes --all node-role.kubernetes.io/master-
```
### 安装存储集群Rook-Ceph
```
sudo kubectl apply -f rook-ceph-operator.yaml
sudo kubectl apply -f rook-ceph-cluster.yaml
```
