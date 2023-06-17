using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UIStack
{
    /// <summary>
    /// ui 를 스택방식으로 동작하게 만들어줄 클래스.
    /// back 키가 눌리면 이벤트를 받아서 탑뷰로 전달한다.
    /// 탑뷰의 onBackPressed 리턴값에 따라 back 처리여부를 결정.
    /// </summary>
    /// <typeparam name="K">스택의 고유 구분 키</typeparam>
    /// <typeparam name="V">스택 뷰 오브젝트</typeparam>
	public class UIStackController<K, V> where K : struct where V : IUIStackable, IOnBackPressed
    {

        private string mName;                           //구분자. 스택이 혼용될때 로그찍을때만 사용함
        private Stack<K> mUIStack;                      //UI순서를 저장할 스택
        private Dictionary<K, V> mUIDictionary;     //UI리스트를 가짐
        private IUIStackChangedHandler<K, V> mHandler;

        public int count { get { return mUIStack.Count; } }
        public int uiCnt { get { return mUIDictionary.Count; } }

        public bool hasStack
        {
            get
            {
                return mUIStack.Count > 0;
            }
        }

        public K topKey
        {
            get
            {
                return mUIStack.Peek();
            }
        }

        public V topValue
        {
            get
            {
                return mUIDictionary[topKey];
            }
        }

        public W getTopValue<W>() where W : V
        {
            return (W)topValue;
        }

        public V elementAt(int index)
        {

            //
            if (mUIDictionary.Count <= index)
                return default(V);
            else
            {
                return mUIDictionary.Values.ElementAt<V>(index);
            }
        }

        private int hideAllBackStackDepth;


        public UIStackController() : this(null, null, null) { }
        public UIStackController(IUIStackChangedHandler<K, V> handler) : this(null, handler, null) { }

        public UIStackController(string name, IUIStackChangedHandler<K, V> handler) { }

        public UIStackController(string name, IUIStackChangedHandler<K, V> handler, IEqualityComparer<K> comparer)
        {
            mName = name;
            mHandler = handler;
            mUIStack = new Stack<K>();
            mUIDictionary = new Dictionary<K, V>(comparer);
        }



        /// <summary>
        /// 스택핸들러 등록.
        /// </summary>
        /// <param name="handler"></param>
        public void setStackChangedHandler(IUIStackChangedHandler<K, V> handler)
        {
            mHandler = handler;
        }




        /// <summary>
        ///  UI를 등록
        /// </summary>
        /// <param name="key">UI 식별 키</param>
        /// <param name="value">UI 오브젝트</param>
        public void registerUI(K key, V value)
        {
            mUIDictionary.Add(key, value);
        }

        private V getValue(K key)
        {
            return mUIDictionary[key];
        }

        /// <summary>
        /// 현재 다운 뷰에 해당 키에 해당하는 뷰가 정상적으로 존재하는지.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool isExistDownView(K key)
        {

            if (!mUIDictionary.ContainsKey(key) || mUIDictionary[key] == null)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 스택을 모두 클리어.
        /// </summary>
		public void clearStack()
        {

            //
            while (hasStack)
            {

                K key = mUIStack.Pop();
                V value = doCloseAndNotify(key, true);

                mHandler.onPopUI(key, value);
                value.onTopReleased();
                value.onPopStack();
            }

            hideAllBackStackDepth = -1;
            lstHideAllViews.Clear();
        }


        /// <summary>
        /// 탑스택을 닫는다.
        /// </summary>
        /// <param name="isPop"></param>
		private void closeTop(bool isPop)
        {
            if (!(this.count > 0)) return;

            //K key = isPop ? mUIStack.Pop () : mUIStack.Peek();
            K key = mUIStack.Peek();
            V panel = doCloseAndNotify(key, isPop);

            //
            panel.onTopReleased();

            //notify
            if (isPop)
            {
                panel.onPopStack();
                mHandler.onPopUI(key, panel);
                mUIStack.Pop();

                //equal 비교를 이렇게 힘들게 해야하다니
                if (hideAllBackStackDepth > mUIStack.Count)
                {
                    setAllBackStackVisible(true);
                    hideAllBackStackDepth = -1;
                }
            }
        }



        /// <summary>
        /// 탑 ui 를 설정한다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="option"></param>
		public bool setUI(K key, UIStack.Option option)
        {
#if DETAIL_LOG
			PrintLog(mName, "setUI(before)", string.Format("[key={0} option={1}] backstack={2} hideAllBackStackDepth={3}", key, option, getBackStackString(), hideAllBackStackDepth));
#else
            PrintLog(mName, "setUI", key + ". option ? : " + option);
#endif

            if (option == Option.HideAllBackStack && hideAllBackStackDepth >= 0)
            {
                Log.d("Option.HideAllBackStack already used. Option.HideBackStack is used instead");
                option = Option.HideBackStack;
            }

            if (!mUIDictionary.ContainsKey(key))
            {
                return false;
            }

            switch (option)
            {

                case UIStack.Option.ClearStack:
                    clearStack();
                    break;

                case UIStack.Option.PopBackStack:
                    closeTop(true);
                    break;

                case UIStack.Option.HideBackStack:
                    closeTop(false);
                    break;

                case UIStack.Option.AddTo:
                    //현재 top에 밀려남만 알리면 됨
                    topValue.onTopReleased();
                    break;

                case UIStack.Option.HideAllBackStack:
                    closeTop(false);
                    setAllBackStackVisible(false);
                    hideAllBackStackDepth = mUIStack.Count;
                    break;
            }

            //
            if (!mUIDictionary.ContainsKey(key))
            {
                return false;
            }

            //
            mUIStack.Push(key);

            //open 처리를 하고 notify
            doOpenAndNotify(topKey, true);

            if (mHandler != null)
            {
                mHandler.onAddUI(key, topValue);
            }

            topValue.onAddStack();
            topValue.onTopRegistered();

#if DETAIL_LOG
            PrintLog(mName, "setUI(after)", string.Format("[key={0} option={1}] backstack={2} hideAllBackStackDepth={3}", key, option, getBackStackString(), hideAllBackStackDepth));
#endif
            return true;
        }



        /// <summary>
        /// 뷰를 닫고 알림.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		private V doCloseAndNotify(K key, bool isPopStack)
        {

            V toCloseUI = mUIDictionary[key];

            string msg = string.Format("[{0}] {1}", key, toCloseUI);
            PrintLog(mName, "closeUI", msg);

            if (mHandler != null)
            {
                mHandler.onSetClosed(key, toCloseUI, isPopStack);
            }

            return toCloseUI;
        }


        /// <summary>
        /// 뷰를 열고 알림.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
		private V doOpenAndNotify(K key, bool isAddStack)
        {

            V toOpenUI = mUIDictionary[key];

            string msg = string.Format("[{0}]{1}", key, toOpenUI);
            PrintLog(mName, "openUI", msg);

            if (mHandler != null)
            {
                mHandler.onSetOpened(key, toOpenUI, isAddStack);
            }

            return toOpenUI;
        }



        /// <summary>
        /// 이전 스택으로 복귀
        /// </summary>
        /// <returns>back 처리여부 리턴</returns>
        public bool goPrev()
        {
#if DETAIL_LOG
            PrintLog(mName, "goPrev", string.Format("backstack={0} hideAllBackStackDepth={1}", getBackStackString(), hideAllBackStackDepth));
#endif

            if (!this.hasStack)
            {
                return false;
            }

            //탑뷰에 묻기
            bool stop = topValue.onBackPressed();
            if (stop)
            {
                return false;
            }


            //back처리 수행
            closeTop(true);

            if (this.hasStack)
            {
                K key = mUIStack.Peek();
                doOpenAndNotify(key, false);
                topValue.onTopRegistered();
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 이전 화면으로 강제이동. 
        /// </summary>
        /// <returns></returns>
		public bool goPrevForced()
        {
#if DETAIL_LOG
            PrintLog(mName, "goPrevForced", string.Format("backstack={0} hideAllBackStackDepth={1}", getBackStackString(), hideAllBackStackDepth));
#endif
            if (this.count <= 1)
            {
                return false;
            }

            //back처리 수행
            closeTop(true);

            if (this.hasStack)
            {
                K key = mUIStack.Peek();
                doOpenAndNotify(key, false);
                topValue.onTopRegistered();
                return true;
            }
            else
            {
                return false;
            }
        }


        //Option.HideAllBackStack을 통해 hide된 녀석들
        private List<V> lstHideAllViews = new List<V>();

        /// <summary>
        /// top ui를 제외한 하단 ui들의 visible/invisible 처리
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private void setAllBackStackVisible(bool state)
        {
            Log.d("setAllBackStackVisible " + state);

            //열려있던 애들만 닫고, 닫았던 애들만 켜줘야 하므로 별도 저장
            if (state)
            {

                for (int i = 0; i < lstHideAllViews.Count; ++i)
                {
                    lstHideAllViews[i].setActiveState(true);
                }
                lstHideAllViews.Clear();

            }
            else
            {

                Stack<K>.Enumerator enumerator = mUIStack.GetEnumerator();

                while (enumerator.MoveNext())
                {

                    V view = mUIDictionary[enumerator.Current];

                    //탑키와 직접비교가 안되서 임시로다가
                    if (view.getActiveState() || view.Equals(topValue))
                    {

                        lstHideAllViews.Add(view);
                        view.setActiveState(state);
                    }
                }
            }
        }


        public Dictionary<K, V>.Enumerator getEnumerator()
        {
            return mUIDictionary.GetEnumerator();
        }

        private static void PrintLog(string name, string methodName, string msg)
        {
            string format = "[{0:S} Stack][{1:S}] : {2:S}";
            Log.d("UIStack", string.Format(format, name, methodName, msg));
        }
    }
}